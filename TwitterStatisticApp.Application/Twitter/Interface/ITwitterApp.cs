using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterStatisticApp.Application.DTO;
using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Application.Twitter.Interface
{
    public interface ITwitterApp
    {
        IEnumerable<UserDTO> AnalyzeFollowers();
        void AnalyzeTweetsByHour();
        void AnalyzeTweetsByTag();
        Task<bool> GetLanguagesSupported();
        Task<bool> GetTweets();
        IEnumerable<TweetsByHourDTO> GetTweetsByHour();
        IEnumerable<TweetsByTagDTO> GetTweetsByTag();
    }
}