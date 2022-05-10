using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Services;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //private IConfiguration _configuration;
        public UserRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
        public string Authorization(AuthClass dataAuth)
        {
            var user = GetByLogin(dataAuth.login, dataAuth.password);
            if (user != null)
            {
                var tokenService = new TokenService();
                return tokenService.BuildToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user);
            }
            else
            {
                return "Невервый логин/пароль";
            }
        }

        public IIncludableQueryable<User, Role> GetAllWithForeignKey()
        {
            return Context.Users.Include(x => x.Role);
        }


        private User GetByLogin(string login, string password)
        {
            return Context.Users.Include(r => r.Role).FirstOrDefault(u => (u.Login == login) && (u.Password == password));
        }
    }
}
