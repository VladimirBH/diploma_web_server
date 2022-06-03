using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using WebServer.DataAccess.Contracts;

namespace WebServer.DataAccess.Implementations.Entities
{
    public class BaseEntity:IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [DataType(DataType.DateTime)]
        [Column("creation_date")]
        public Instant CreationDate { get; set; }
    }
}
