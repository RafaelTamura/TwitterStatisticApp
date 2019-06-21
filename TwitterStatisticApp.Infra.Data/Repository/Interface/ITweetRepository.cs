using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface ITweetRepository: IRepositoryBase<Tweet>
    {
        void RemoveAll();
    }
}
