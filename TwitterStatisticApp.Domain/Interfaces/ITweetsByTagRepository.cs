using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Domain.Interfaces
{
    public interface ITweetsByTagRepository : IRepositoryBase<TweetsByTag>
    {
        void RemoveAll();
    }
}
