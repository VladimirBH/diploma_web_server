using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.Classes
{
    [Table("calculations_history")]
    public class CalculationHistory : BaseEntity
    {
        [Column("user_id")]
        [Required(ErrorMessage = "User's id is required")]
        public int UserId { set; get; }
        
        [Column("furnace_id")]
        [Required(ErrorMessage = "Furnace's id is required")]
        public int FurnaceId { set; get; }
        
        [Column("ag")]
        public decimal? Ag { set; get; }
        
        [Column("al")]
        public decimal? Al { set; get; }
        
        [Column("au")]
        public decimal? Au { set; get; }
        
        [Column("ca")]
        public decimal? Ca { set; get; }

        [Column("cr")]
        public decimal? Cr { set; get; }

        [Column("cu")]
        public decimal? Cu { set; get; }

        [Column("fe")]
        public decimal? Fe { set; get; }

        [Column("ni")]
        public decimal? Ni { set; get; }

        [Column("pb")]
        public decimal? Pb { set; get; }

        [Column("si")]
        public decimal? Si { set; get; }

        [Column("sn")]
        public decimal? Sn { set; get; }

        [Column("zn")]
        public decimal? Zn { set; get; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; } 
        
        [ForeignKey("FurnaceId")]
        public Furnace? Furnace { get; set; } 
    }
}
