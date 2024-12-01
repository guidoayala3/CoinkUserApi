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
            // Configura la entidad UserView para mapearla a la vista 'user_view'
            modelBuilder.Entity<UserView>(entity =>
            {
                // Especifica que esta entidad no tiene clave primaria porque es una vista
                entity.HasNoKey();
                
                // Mapea la entidad a la vista en la base de datos
                entity.ToView("user_view");

                // Opcional: Si las columnas tienen nombres diferentes, puedes configurar las propiedades con el nombre de columna correspondiente
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
