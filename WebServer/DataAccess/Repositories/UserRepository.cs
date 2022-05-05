using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.DBContext;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private readonly ApplicationContext appContext;
        public UserRepository(ApplicationContext context) : base(context)
        {
            this.appContext = context;
        }



        public bool Authorization(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
