using Microsoft.Extensions.Configuration;
using System.Linq;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Infra.Data.Repository.Interface;

namespace TwitterStatisticApp.Infra.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        { }

        public User GetById(string id)
        {
            var user = this.Find(q => q.IdStr == id);
            return user != null ? user.ToList().FirstOrDefault() : null;
        }

        public void RemoveAll()
        {
            this.Remove(q => true);
        }
    }
}
