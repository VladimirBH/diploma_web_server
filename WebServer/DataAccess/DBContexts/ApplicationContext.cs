using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebServer.DBContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}
