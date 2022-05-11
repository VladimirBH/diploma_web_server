using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json.Linq;
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

        public JObject Authorization(AuthClass dataAuth)
        {
            dynamic jsonObject = new JObject();
            var user = GetByLogin(dataAuth.login, dataAuth.password);
            if (user != null)
            {
                var tokenService = new TokenService();
                jsonObject.Add("AccessToken", tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user));
                jsonObject.Add("RefreshToken", tokenService.BuildRefreshToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user));
                jsonObject.Add("ExpiredInAccessToken", int.Parse(Configuration["JWT:AccessTokenLifeTime"]));
                jsonObject.Add("ExpiredInRefreshToken", int.Parse(Configuration["JWT:RefreshTokenLifeTime"]));
                return jsonObject;
            }
            else
            {
                jsonObject.Add("Error", "Неверный логин/пароль");
                return jsonObject;
            }
        }

        private string GetAccessToken(User user)
        {
            var tokenService = new TokenService();
            return tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user);
        }

        public JObject RefreshPairTokens(string refreshToken)
        {
            var tokenService = new TokenService();
            dynamic jsonObject = new JObject();
            if (tokenService.IsTokenValid(Configuration["JWT:Key"], Configuration["JWT:Issuer"], refreshToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(refreshToken);
                var id = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
                if (id == null)
                {
                    jsonObject.Error = "Доступ запрещен";
                    return jsonObject;
                }

                var user = GetById(int.Parse(id));
                user = GetByLogin(user.Login, user.Password);
                jsonObject.AccessToken = tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user);
                jsonObject.RefreshToken = tokenService.BuildRefreshToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user);
                jsonObject.ExpiredInAccessToken = int.Parse(Configuration["JWT:AccessTokenLifeTime"]);
                jsonObject.ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"]);
                return jsonObject;
            }
            else
            {
                jsonObject.Error = "Доступ запрещен";
                return jsonObject;
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
