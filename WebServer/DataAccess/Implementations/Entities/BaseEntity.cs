using WebServer.DataAccess.Contracts;

namespace WebServer.DataAccess.Implementations.Entities
{
    public class BaseEntity:IEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
