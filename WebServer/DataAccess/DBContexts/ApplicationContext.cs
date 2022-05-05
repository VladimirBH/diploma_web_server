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
        public DbSet<User> user { get; set; }
        public DbSet<Role> role { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany(b => b.Users)
                .HasForeignKey(t => t.RoleId);
        }
        
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
            //optionsBuilder.UseNpgsql("Host=db.jlwcfevbewaryulsslre.supabase.co;Port=6543;Database=database;Username=postgres;Password=1977213vovaFifer");
        }*/
        
    }
}
