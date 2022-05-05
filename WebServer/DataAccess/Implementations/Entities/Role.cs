using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.DataAccess.Implementations.Entities
{
    [Table("roles")]
    public class Role : BaseEntity
    {
        [Column("name_role")]
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }


        public List<User> Users { get; set; }
    }
}
