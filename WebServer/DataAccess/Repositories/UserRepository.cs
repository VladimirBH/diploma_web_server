using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebServer.Classes;
using WebServer.DataAccess.Contracts;
using WebServer.DataAccess.DBContexts;
using WebServer.DataAccess.Implementations.Entities;
using WebServer.Services;
using WebServer.Exceptions;

namespace WebServer.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
        public TokenPair Authorization(AuthClass dataAuth)
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
                    ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"]),
                    IdRole = user.RoleId,
                    CreationDateTime = DateTime.Now
                };
                return tokenPair;
            }
            else
            {
                throw new UserException("Unforbidden");
            }
        }

        public int GetUserIdFromRefreshToken(string refreshToken)
        {
            var tokenService = new TokenService(Configuration);
            if (tokenService.IsTokenValid(Configuration["JWT:Key"], Configuration["JWT:Issuer"], refreshToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(refreshToken);
                var id = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
                if (id == null)
                {
                    throw new UserException("Unauthorized");
                }

                return int.Parse(id);
            }
            else
            {
                throw new UserException("Unauthorized");
            }
        }

        public TokenPair RefreshPairTokens(string refreshToken)
        {
            var tokenService = new TokenService(Configuration);
            var user = GetById(GetUserIdFromRefreshToken(refreshToken));
            user = GetByLogin(user.Login, user.Password);
            var tokenPair = new TokenPair
            {
                AccessToken = tokenService.BuildAccessToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                RefreshToken = tokenService.BuildRefreshToken(Configuration["JWT:Key"], Configuration["JWT:Issuer"], user),
                ExpiredInAccessToken = int.Parse(Configuration["JWT:AccessTokenLifeTime"]),
                ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"]),
                IdRole = user.RoleId,
                CreationDateTime = DateTime.Now
            };
            return tokenPair;
        }

        public IIncludableQueryable<User, Role> GetAllWithForeignKey()
        {
            return Context.Users.Include(x => x.Role);
        }


        private User GetByLogin(string login, string password)
        {
            return Context.Users.Include(r => r.Role).FirstOrDefault(u => (u.Login == login) && (u.Password == password));
        }

        public User GetCurrentUserInfo(string refreshToken)
        {
            return Context.Users.Include(r => r.Role)
                .FirstOrDefault(u => u.Id == GetUserIdFromRefreshToken(refreshToken));
        }
    }
}
