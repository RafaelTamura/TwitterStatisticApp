using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Application.Twitter.Interface;
using TwitterStatisticApp.Properties;
using TwitterStatisticApp.ViewModels;

namespace TwitterStatisticApp.Controllers
{
    [Route("api/[controller]")]
    public class TwitterController : Controller
    {
        private readonly ITwitterApp _twitterApp;
        private readonly IMapper _mapper;

        public TwitterController(ITwitterApp twitterApp, IMapper mapper)
        {
            _twitterApp = twitterApp;
            _mapper = mapper;
        }

        [HttpPost("Carregar")]
        public async Task<IActionResult> Carregar()
        {
            try
            {
                var retorno = await _twitterApp.GetLanguagesSupported();
                if (!retorno)
                {
                    return BadRequest(Resources.Carregar_ErroGetLanguages);
                }

                retorno = await _twitterApp.GetTweets();
                if (!retorno)
                {
                    return BadRequest(Resources.Carregar_ErroGetTweets);
                }

                _twitterApp.AnalyzeTweetsByHour();
                _twitterApp.AnalyzeTweetsByTag();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, Resources.Carregar_ErroInterno);
            }
        }

        [HttpGet("Estatistica/UsuariosMaisSeguidores")]
        public ActionResult<IEnumerable<UserViewModel>> ObterUsuariosMaisSeguidores()
        {
            try
            {
                var seguidores = _twitterApp.AnalyzeFollowers();
                if (seguidores == null || seguidores.Count() == 0)
                {
                    return BadRequest(Resources.ObterMaisSeguidores_ErroObter);
                }

                return Ok(JsonConvert.SerializeObject(_mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserViewModel>>(seguidores)));
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, Resources.ObterMaisSeguidores_ErroInterno);
            }
        }

        [HttpGet("Estatistica/TweetsPorHora")]
        public IActionResult ObterTweetsPorHora()
        {
            try
            {
                var tweetsByHours = _twitterApp.GetTweetsByHour();
                if (tweetsByHours == null || tweetsByHours.Count() == 0)
                {
                    return BadRequest(Resources.ObterTweetsPorHora_ErroObter);
                }

                var map = _mapper.Map<IEnumerable<TweetsByHourDTO>, IEnumerable<TweetsByHourViewModel>>(tweetsByHours);

                return Ok(JsonConvert.SerializeObject(map.OrderByDescending(q => q.Date).ThenByDescending(x => x.Hour)));
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, Resources.ObterTweetsPorHora_ErroInterno);
            }
        }

        [HttpGet("Estatistica/TweetsPorTag")]
        public IActionResult ObterTweetsPorTag()
        {
            try
            {
                var tweetsByTag = _twitterApp.GetTweetsByTag();
                if (tweetsByTag == null || tweetsByTag.Count() == 0)
                {
                    return BadRequest(Resources.ObterTweetsPorTag_ErroObter);
                }

                var map = _mapper.Map<IEnumerable<TweetsByTagDTO>, IEnumerable<TweetsByTagViewModel>>(tweetsByTag);

                return Ok(JsonConvert.SerializeObject(map.OrderBy(q => q.Hashtag).ThenBy(x => x.LanguageName).ThenBy(l => l.Location)));
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, Resources.ObterTweetsPorHora_ErroInterno);
            }
        }
    }
}
