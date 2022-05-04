using Microsoft.EntityFrameworkCore;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.DBContexts
{
    public class UserContext : DbContext
    {
        public UserContext() : base()
        { }  
        
        public DbSet<Users>? Users { get; set; }
    }
}
