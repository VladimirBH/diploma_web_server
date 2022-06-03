using NodaTime;

namespace WebServer.DataAccess.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }

        Instant CreationDate { get; set; }
    }
}
