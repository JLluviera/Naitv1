using Microsoft.EntityFrameworkCore;
using Naitv1.Models;


namespace Naitv1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
  
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }

        public DbSet<Notificaciones> Notificaciones { get; set; }

        public DbSet<Ciudades> Ciudades { get; set; }

        public DbSet<PlantillaNotificacion> PlantillaNotificacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Anfitrion)
                .WithMany(u => u.ActividadesDelUsuario)
                .HasForeignKey(a => a.AnfitrionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ActividadesDelUsuario)
                .WithOne(a => a.Anfitrion)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
