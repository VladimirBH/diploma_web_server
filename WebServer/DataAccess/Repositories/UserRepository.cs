using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
        public JsonDocument Authorization(AuthClass dataAuth)
        {
            var user = GetByLogin(dataAuth.login, dataAuth.password);
            if (user != null)
            {
                var tokenService = new TokenService(Configuration);
                var tokenPair = new TokenPair
                {
                    AccessToken = tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                    RefreshToken = tokenService.BuildRefreshToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                    ExpiredInAccessToken = int.Parse(Configuration["JWT:AccessTokenLifeTime"]),
                    ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"])
                };
                var jsonString = JsonConvert.SerializeObject(tokenPair);
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            else
            {
                var jsonObject = JsonDocument.Parse("{ \"Error\": \"Неверный логин/пароль\" }");
                return jsonObject;
            }
        }

        public JsonDocument RefreshPairTokens(string refreshToken)
        {
            var tokenService = new TokenService(Configuration);
            if (tokenService.IsTokenValid(Configuration["JWT:Key"], Configuration["JWT:Issuer"], refreshToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(refreshToken);
                var id = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
                if (id == null)
                {
                    var jsonObject = JsonDocument.Parse("{ \"Error\": \"Доступ запрещен\" }");
                    return jsonObject;
                }

                var user = GetById(int.Parse(id));
                user = GetByLogin(user.Login, user.Password);
                var tokenPair = new TokenPair
                {
                    AccessToken = tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                    RefreshToken = tokenService.BuildRefreshToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                    ExpiredInAccessToken = int.Parse(Configuration["JWT:AccessTokenLifeTime"]),
                    ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"])
                };
                var jsonString = JsonConvert.SerializeObject(tokenPair);
                var json = JsonDocument.Parse(jsonString);
                return json;
            }
            else
            {
                var jsonObject = JsonDocument.Parse("{ \"Error\": \"Доступ запрещен\" }");
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
