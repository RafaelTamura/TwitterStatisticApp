using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Domain.Interfaces
{
    public interface ITweetRepository: IRepositoryBase<Tweet>
    {
        void RemoveAll();
    }
}
