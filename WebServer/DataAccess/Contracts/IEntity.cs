namespace WebServer.DataAccess.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreationDate { get; set; }
    }
}
