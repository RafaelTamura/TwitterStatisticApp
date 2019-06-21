using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetById(string id);
        void RemoveAll();
    }
}
