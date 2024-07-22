using Microsoft.EntityFrameworkCore;
using ByteStormBackend.Models;

namespace ByteStormBackend.Data
{
    public class ByteStormContext : DbContext
    {
        public ByteStormContext(DbContextOptions<ByteStormContext> options) : base(options) { }
        public DbSet<Mision> Misiones { get; set; }
        public DbSet<Operativo> Operativos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
    }
}