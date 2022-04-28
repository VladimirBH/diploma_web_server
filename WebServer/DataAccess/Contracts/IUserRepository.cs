using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool Authorization(string login, string password);
    }
}
