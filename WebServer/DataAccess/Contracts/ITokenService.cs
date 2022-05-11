using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.Contracts
{
    public interface ITokenService
    {
        string BuildAccessToken(string key, string issuer, User user);
        string BuildRefreshToken(string key, string issuer, User user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
