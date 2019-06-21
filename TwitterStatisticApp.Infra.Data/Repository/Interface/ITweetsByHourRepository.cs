using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface ITweetsByHourRepository : IRepositoryBase<TweetsByHour>
    {
        void RemoveAll();
    }
}
