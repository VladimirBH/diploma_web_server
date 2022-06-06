using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.DataAccess.Implementations.Entities;

[Table("furnaces")]
public class Furnace : BaseEntity
{
    [Column("name_furnace")]
    [Required(ErrorMessage = "Furnace's name is required")]
    public string FurnaceName { get; set; }
}