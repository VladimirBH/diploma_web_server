using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DBContext
{
    public class ApplicationContext : DbContext
    {
        //private readonly IConfiguration configuration;



        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //this.configuration = _configuration;
        }
        public DbSet<Users>? Users { get; set; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
            //optionsBuilder.UseNpgsql("Host=db.jlwcfevbewaryulsslre.supabase.co;Port=6543;Database=database;Username=postgres;Password=1977213vovaFifer");
        }*/
    }
}
