namespace WebServer.DataAccess.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
    }
}
