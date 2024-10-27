using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;

namespace ByteStormBackend.Data
{
    public class ByteStormContext : DbContext
    {
        public ByteStormContext(DbContextOptions<ByteStormContext> options)
            : base(options)
        {
        }

        public DbSet<Mision> Misiones { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Operativo> Operativos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración Operativo y Misiones
            modelBuilder.Entity<Mision>()
                .HasOne(m => m.Operativo)
                .WithMany(o => o.Misiones)
                .HasForeignKey(m => m.OperativoID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración Mision y Equipos
            modelBuilder.Entity<Equipo>()
                .HasOne(e => e.Mision)
                .WithMany(m => m.Equipos)
                .HasForeignKey(e => e.MisionID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}