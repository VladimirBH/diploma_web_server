using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.DBContext;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _appContext;
        public UserRepository(ApplicationContext context) : base(context)
        {
            this._appContext = context;
        }



        public bool Authorization(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
