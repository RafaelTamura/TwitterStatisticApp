using Microsoft.Extensions.Configuration;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Domain.Interfaces;

namespace TwitterStatisticApp.Infra.Data.Repository
{
    public class TweetRepository : RepositoryBase<Tweet>, ITweetRepository
    {
        public TweetRepository(IConfiguration configuration) : base(configuration)
        { }

        public void RemoveAll()
        {
            this.Remove(q => true);
        }
    }
}
