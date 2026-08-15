using Microsoft.EntityFrameworkCore;
using testMika.DAL.Entities;

namespace testMika.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
