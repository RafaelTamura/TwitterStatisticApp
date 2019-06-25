using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Domain.Interfaces
{
    public interface ITweetsByHourRepository : IRepositoryBase<TweetsByHour>
    {
        void RemoveAll();
    }
}
