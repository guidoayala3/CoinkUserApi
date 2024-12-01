using Microsoft.EntityFrameworkCore;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define el DbSet para la vista 'user_view'
        public DbSet<UserView> UserViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserView>(entity =>
            {
                entity.HasNoKey();
                
                entity.ToView("user_view");

                 entity.Property(e => e.Id).HasColumnName("user_id");
                entity.Property(e => e.Name).HasColumnName("user_name");
                entity.Property(e => e.Phone).HasColumnName("user_phone");
                entity.Property(e => e.Address).HasColumnName("user_address");
                entity.Property(e => e.Municipality).HasColumnName("municipality_name");
                entity.Property(e => e.Department).HasColumnName("department_name");
                entity.Property(e => e.Country).HasColumnName("country_name");
            });
        }
    }
}
