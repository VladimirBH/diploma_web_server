using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebServer.DataAccess.Contracts;

namespace WebServer.DataAccess.Implementations.Entities
{
    public class BaseEntity:IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }
    }
}
