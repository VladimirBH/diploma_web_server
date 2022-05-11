using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json.Linq;
using WebServer.Classes;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        JsonObject Authorization(AuthClass dataAuth);
        IIncludableQueryable<User, Role> GetAllWithForeignKey();
        JObject RefreshAccessToken(string refreshToken);
    }
}
