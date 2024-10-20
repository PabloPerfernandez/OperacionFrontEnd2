using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class Equipo
    {
        [Key]
        public int equipoCodigo { get; set; }  // Primary Key
        public int equipoTipo { get; set; }
        public string? equipoDescripcion { get; set; }
        public int EstadoEquipo { get; set; }

        // Clave foránea hacia Mision
        public int? MisionID { get; set; }
        public Mision? Mision { get; set; }

        public Equipo()
        {
            equipoDescripcion = string.Empty;
        }
    }
}