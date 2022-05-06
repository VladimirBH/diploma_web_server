using WebServer.Classes;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        string Authorization(AuthClass dataAuth);
        User GetByLogin(string login);
    }
}
