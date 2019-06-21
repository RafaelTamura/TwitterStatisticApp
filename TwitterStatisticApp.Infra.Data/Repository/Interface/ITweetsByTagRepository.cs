using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface ITweetsByTagRepository : IRepositoryBase<TweetsByTag>
    {
        void RemoveAll();
    }
}
