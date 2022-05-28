using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json.Linq;
using WebServer.Classes;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        TokenPair Authorization(AuthClass dataAuth);
        IIncludableQueryable<User, Role> GetAllWithForeignKey();
        TokenPair RefreshPairTokens(string refreshToken);
        User GetCurrentUserInfo(string refreshToken);
    }
}
