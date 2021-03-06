using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
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
        public UserRepository(ApplicationContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
        public TokenPair Authorization(AuthClass dataAuth)
        {
            var user = GetByLogin(dataAuth.login);
            if (user == null) throw new AuthenticationException();
            if (!BCrypt.Net.BCrypt.Verify(dataAuth.password, user.Password)) throw new AuthenticationException();
            var tokenService = new TokenService(Configuration);
            var tokenPair = new TokenPair
            {
                AccessToken = tokenService.BuildAccessToken(Configuration["JWT:Key"],
                    Configuration["JWT:Issuer"], user),
                RefreshToken = tokenService.BuildRefreshToken(Configuration["JWT:Key"],
                    Configuration["JWT:Issuer"], user),
                ExpiredInAccessToken = int.Parse(Configuration["JWT:AccessTokenLifeTime"]),
                ExpiredInRefreshToken = int.Parse(Configuration["JWT:RefreshTokenLifeTime"]),
                IdRole = user.RoleId,
                CreationDateTime = DateTime.Now
            };
            return tokenPair;
        }

        public int GetUserIdFromRefreshToken(string refreshToken)
        {
            var tokenService = new TokenService(Configuration);
            if (!tokenService.IsTokenValid(Configuration["JWT:Key"], Configuration["JWT:Issuer"], refreshToken))
                throw new AuthenticationException();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(refreshToken);
            var id = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            if (id == null)
            {
                throw new AuthenticationException();
            }

            return int.Parse(id);

        }
        
        public int GetUserIdFromAccessToken(string accessToken)
        {
            var tokenService = new TokenService(Configuration);
            if (!tokenService.IsTokenValid(Configuration["JWT:Key"], Configuration["JWT:Issuer"], accessToken))
                throw new AuthenticationException();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(accessToken);
            var login = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            if (login == null)
            {
                throw new AuthenticationException();
            }

            return GetByLogin(login).Id;

        }


        public TokenPair RefreshPairTokens(string refreshToken)
        {
            var tokenService = new TokenService(Configuration);
            var user = GetById(GetUserIdFromRefreshToken(refreshToken));
            user = GetByLogin(user.Login);
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


        private User GetByLogin(string login)
        {
            return Context.Users.Include(r => r.Role).FirstOrDefault(u => (u.Login == login));
        }

        public User GetCurrentUserInfo(string accessToken)
        {
            return Context.Users.Include(r => r.Role)
                .FirstOrDefault(u => u.Id == GetUserIdFromAccessToken(accessToken));
        }
    }
}
