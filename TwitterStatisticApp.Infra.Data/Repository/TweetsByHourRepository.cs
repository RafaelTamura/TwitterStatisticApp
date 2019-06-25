using Microsoft.Extensions.Configuration;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Domain.Interfaces;

namespace TwitterStatisticApp.Infra.Data.Repository
{
    public class TweetsByHourRepository : RepositoryBase<TweetsByHour>, ITweetsByHourRepository
    {
        public TweetsByHourRepository(IConfiguration configuration) : base(configuration)
        { }

        public void RemoveAll()
        {
            this.Remove(q => true);
        }
    }
}
