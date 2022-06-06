using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using WebServer.Classes;
using WebServer.DataAccess.Implementations.Entities;

namespace WebServer.DataAccess.DBContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Furnace>? Furnaces { get; set; }
        public DbSet<CalculationHistory>? CalculationHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateBirth).HasColumnType("date");
                entity.Property(e => e.CreationDate).HasColumnType("datetime with time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime with time zone");
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime with time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime with time zone");
            });
            modelBuilder.Entity<Furnace>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime with time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime with time zone");
            });
            modelBuilder.Entity<CalculationHistory>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime with time zone");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime with time zone");
            });
        }

    }
}
