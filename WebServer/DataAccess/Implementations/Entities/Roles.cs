using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.DataAccess.Implementations.Entities
{
    [Table("roles")]
    public class Roles : BaseEntity
    {
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
    }
}
