using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.DBContext;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public bool Authorization(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
