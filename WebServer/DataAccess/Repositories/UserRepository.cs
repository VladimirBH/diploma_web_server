using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Services;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _appContext;
        public UserRepository(ApplicationContext context) : base(context)
        {
            
            this._appContext = context;
        }
        public string Authorization(AuthClass dataAuth)
        {
            User user = GetByLogin(dataAuth.login);
            if (user != null)
            {
                if (user.Password == dataAuth.password)
                {
                    var tokenService = new TokenService();
                    return tokenService.BuildToken("supersecretkeyclientdontthink123", "https://localhost:7019", user);
                }
                else
                {
                    return "Невервый логин/пароль";
                }
            }
            else
            {
                return "Невервый логин/пароль";
            }

            throw new NotImplementedException();
        }

        public User GetByLogin(string login)
        {
            return _appContext.Users.FirstOrDefault(u => u.Login == login);
        }
    }
}
