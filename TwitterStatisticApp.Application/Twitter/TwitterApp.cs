using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Application.Twitter.Interface;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Domain.Interfaces;

namespace TwitterStatisticApp.Application.Twitter
{
    public class TwitterApp : ITwitterApp
    {
        private readonly IConfiguration _config;
        private readonly ILanguageRepository _languageRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITweetsByHourRepository _tweetsByHourRepository;
        private readonly ITweetsByTagRepository _tweetsByTagRepository;
        private readonly IMapper _mapper;

        public TwitterApp(IConfiguration config,
                          ILanguageRepository languageRepository,
                          ITweetRepository tweetRepository,
                          IUserRepository userRepository,
                          ITweetsByHourRepository tweetsByHourRepository,
                          ITweetsByTagRepository tweetsByTagRepository,
                          IMapper mapper)
        {
            _config = config;
            _languageRepository = languageRepository;
            _tweetRepository = tweetRepository;
            _userRepository = userRepository;
            _tweetsByHourRepository = tweetsByHourRepository;
            _tweetsByTagRepository = tweetsByTagRepository;
            _mapper = mapper;
        }

        public async Task<bool> GetLanguagesSupported()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", GetTwitterAuthHeader(_config["TwitterService:Languages"]));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                var result = await httpClient.GetAsync(_config["TwitterService:Languages"]);
                var response = await result.Content.ReadAsStringAsync();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var languages = JsonConvert.DeserializeObject<IEnumerable<LanguageDTO>>(response);
                    foreach (var lang in languages)
                    {
                        var map = _mapper.Map<LanguageDTO, Language>(lang);
                        map.SetId(Guid.NewGuid());

                        if (_languageRepository.GetByCode(map.Code) == null)
                        {
                            _languageRepository.Add(map);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> GetTweets()
        {
            string token = await GetAuthorization();

            string[] tags = _config["Tags"].Split(',');

            #region Remover a base de dados de análise, antes de iniciar nova análise
            _userRepository.RemoveAll();
            _tweetRepository.RemoveAll();
            #endregion

            foreach (var tag in tags)
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(_config["TwitterService:Tweets"], tag);

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAF2J%2FAAAAAAArvJ6%2BZG122CmG%2FCZguGfkOAloJE%3DNBk4LAg4JgvUMGpFbSpBjcNxKfLJOT1VmvWfIOHXEvQt1SJQez" /*GetTwitterAuthHeader(url)*/);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

                    var result = await httpClient.GetAsync(url);
                    var response = await result.Content.ReadAsStringAsync();

                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var tweets = JsonConvert.DeserializeObject<Dictionary<string, object>>(response)["statuses"];
                        var objs = tweets as IEnumerable<dynamic>;
                        foreach (var tweet in objs.Select(t => new { CreatedAt = (string)(t["created_at"].ToString()),
                                                                     IdStr = (string)(t["id_str"].ToString()),
                                                                     Entities = (string)(t["entities"].ToString()),
                                                                     User = (string)(t["user"].ToString())}))
                        {
                            var tweetDTO = new TweetDTO();
                            tweetDTO.CreatedAt = tweet.CreatedAt;

                            var entities = JsonConvert.DeserializeObject<EntityDTO>(tweet.Entities);

                            tweetDTO.Hashtags = new List<HashtagDTO>();

                            foreach (var hashtag in entities.Hashtags)
                            {
                                tweetDTO.Hashtags.Add(new HashtagDTO() { Text = hashtag.Text });
                            }

                            var user = JsonConvert.DeserializeObject<UserTweetDTO>(tweet.User);

                            tweetDTO.User = _mapper.Map<UserTweetDTO, UserDTO>(user);

                            var map = _mapper.Map<TweetDTO, Tweet>(tweetDTO);
                            map.SetId(Guid.NewGuid());

                            _tweetRepository.Add(map);

                            var mapUser = _mapper.Map<UserDTO, User>(tweetDTO.User);
                            mapUser.SetId(Guid.NewGuid());

                            if (_userRepository.GetById(mapUser.IdStr) == null)
                            {
                                _userRepository.Add(mapUser);
                            }
                        }
                    }
                }
            }

            return true;
        }

        public IEnumerable<UserDTO> AnalyzeFollowers()
        {
            var users = _userRepository.GetAll().OrderByDescending(q => q.FollowersCount).ToList();

            if (users != null && users.Count > 0)
            {
                var list = new List<UserDTO>();

                for (int i = 0; i < 5; i++)
                {
                    var user = _mapper.Map<User, UserDTO>(users[i]);
                    list.Add(user);
                }

                return list;
            }

            return null;
        }

        public void AnalyzeTweetsByHour()
        {
            _tweetsByHourRepository.RemoveAll();

            var listByHour = _tweetRepository.GetAll()
                                .GroupBy(q => new { date = DateTime.ParseExact(q.CreatedAt, "ddd MMM dd HH:mm:ss +0000 yyyy", CultureInfo.InvariantCulture).Date,
                                                    hour = DateTime.ParseExact(q.CreatedAt, "ddd MMM dd HH:mm:ss +0000 yyyy", CultureInfo.InvariantCulture).Hour })
                                .Select(s => new { date = s.Key.date, hour = s.Key.hour, count = s.Count() });

            if (listByHour != null && listByHour.ToList().Count() > 0)
            {
                foreach (var item in listByHour.ToList())
                {
                    var tweetByHour = new TweetsByHour(Guid.NewGuid(), item.date, item.hour, item.count);
                    _tweetsByHourRepository.Add(tweetByHour);
                }
            }
        }

        public void AnalyzeTweetsByTag()
        {
            _tweetsByTagRepository.RemoveAll();

            var listTweets = _tweetRepository.GetAll().ToList();

            if (listTweets != null && listTweets.Count() > 0)
            {
                var listByTag = new List<TweetsByTagDTO>();
                string[] tags = _config["Tags"].Split(',');

                foreach (var tweet in listTweets)
                {
                    var user = _userRepository.GetById(tweet.UserIdStr);
                    if (user != null)
                    {
                        foreach (var tag in tweet.Hashtags)
                        {
                            if (tags.FirstOrDefault(q => q.ToLower() == tag.Text.ToLower()) != null)
                            {
                                var tweetsByTagDTO = new TweetsByTagDTO()
                                {
                                    Id = Guid.NewGuid(),
                                    Hashtag = tag.Text,
                                    Location = user.Location,
                                    LanguageCode = user.LanguageCode
                                };

                                listByTag.Add(tweetsByTagDTO);
                            }
                        }
                    }
                }

                var tweetsFiltered = listByTag.GroupBy(q => new { hashtag = q.Hashtag, language = q.LanguageCode, location = q.Location })
                                              .Select(s => new { hashtag = s.Key.hashtag, language = s.Key.language, location = s.Key.location, count = s.Count() });

                if (tweetsFiltered != null)
                {
                    foreach (var tweetFiltered in tweetsFiltered)
                    {
                        var lang = _languageRepository.GetByCode(tweetFiltered.language);
                        if (lang != null)
                        {
                            var tweetsByTag = new TweetsByTag(Guid.NewGuid(), tweetFiltered.count, tweetFiltered.hashtag, tweetFiltered.location, lang.Code, lang.Name);
                            _tweetsByTagRepository.Add(tweetsByTag);
                        }
                    }
                }
            }
        }

        public IEnumerable<TweetsByHourDTO> GetTweetsByHour()
        {
            var tweetsByHour = _tweetsByHourRepository.GetAll();
            if (tweetsByHour != null)
            {
                var list = _mapper.Map<IEnumerable<TweetsByHour>, IEnumerable<TweetsByHourDTO>>(tweetsByHour);
                return list;
            }

            return null;
        }

        public IEnumerable<TweetsByTagDTO> GetTweetsByTag()
        {
            var tweetsByTag = _tweetsByTagRepository.GetAll();
            if (tweetsByTag != null)
            {
                var list = _mapper.Map<IEnumerable<TweetsByTag>, IEnumerable<TweetsByTagDTO>>(tweetsByTag);
                return list;
            }

            return null;
        }

        private async Task<string> GetAuthorization()
        {
            string keyEncoded = HttpUtility.UrlEncode(_config["ApiKey"]);
            string secretEncoded = HttpUtility.UrlEncode(_config["ApiSecretKey"]);

            string keySecretEncoded = Base64Encode(keyEncoded + ":" + secretEncoded);
            string credentials = ("Basic " + keySecretEncoded);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", credentials);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

                var values = new Dictionary<string, string> { { "grant_type", "client_credentials" } };
                var body = new FormUrlEncodedContent(values);

                var result = await httpClient.PostAsync(_config["TwitterService:GetToken"], body);
                var response = await result.Content.ReadAsStringAsync();

                //Deserialize JSON into TwitterResponse object
                TwitterResponseDTO twitterResponse = JsonConvert.DeserializeObject<TwitterResponseDTO>(response);

                string bearer = twitterResponse.access_token;

                return bearer;
            }
        }

        private string GetTwitterAuthHeader(string url)
        {
            string oauthConsumerKey = _config["ApiKey"];
            string oauthConsumerSecret = _config["ApiSecretKey"];
            string oauthToken = _config["AccessToken"];
            string oauthTokenSecret = _config["AccessTokenSecret"];
            const string oauthVersion = "1.0";
            const string oauthSignatureMethod = "HMAC-SHA1";

            var oauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauthTimestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            string resourceUrl = url;
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}";

            var baseString = string.Format(baseFormat,
                                        oauthConsumerKey,
                                        oauthNonce,
                                        oauthSignatureMethod,
                                        oauthTimestamp,
                                        oauthToken,
                                        oauthVersion
                                        );

            baseString = string.Concat("GET&", Uri.EscapeDataString(resourceUrl),
                         "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauthConsumerSecret),
                                    "&", Uri.EscapeDataString(oauthTokenSecret));

            string oauthSignature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                   "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                   "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                   "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauthNonce),
                                    Uri.EscapeDataString(oauthSignatureMethod),
                                    Uri.EscapeDataString(oauthTimestamp),
                                    Uri.EscapeDataString(oauthConsumerKey),
                                    Uri.EscapeDataString(oauthToken),
                                    Uri.EscapeDataString(oauthSignature),
                                    Uri.EscapeDataString(oauthVersion)
                            );

            return authHeader;
        }

        private string Base64Encode(string stringText)
        {
            var stringTextBytes = System.Text.Encoding.UTF8.GetBytes(stringText);
            return System.Convert.ToBase64String(stringTextBytes);
        }
    }
}
