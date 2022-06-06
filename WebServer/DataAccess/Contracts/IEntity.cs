using NodaTime;

namespace WebServer.DataAccess.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTimeOffset CreationDate { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
    }
}
