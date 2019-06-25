using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetById(string id);
        void RemoveAll();
    }
}
