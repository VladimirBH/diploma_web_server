using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.DataAccess.Implementations.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        
        [Column("surname")]
        public string Surname { get; set; }
        
        [Column("patronymic")]
        public string Patronymic { get; set; }
        
        [Column("date_birth")]
        public DateTime DateBirth { get; set; }
        
        [Column("login")]
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        
        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Column("role_id")]
        [Required] 
        public int RoleId { get; set; }
        
        public Role Role { get; set; }

        /*[Column("role_id")] 
        [Required] 
        public int RoleId { get; set; }
        
        [ForeignKey("role_id")]
        public Role Role { get; set; } */
    }
}
