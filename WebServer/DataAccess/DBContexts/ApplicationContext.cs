using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DBContext
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Users>? Users { get; set; }

        public ApplicationContext(IConfiguration _configuration)
        {
            Database.EnsureCreated();
            this.configuration = _configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
            optionsBuilder.UseNpgsql("Host=db.jlwcfevbewaryulsslre.supabase.co;Port=6543;Database=database;Username=postgres;Password=1977213vovaFifer");
        }
    }
}
