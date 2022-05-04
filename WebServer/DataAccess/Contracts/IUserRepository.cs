using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        bool Authorization(string login, string password);
    }
}
