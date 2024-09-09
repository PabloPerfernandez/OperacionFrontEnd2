using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class Equipo
    {
        [Key]
        public int EquipoCodigo { get; set; }  // Primary Key
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
        public int EstadoEquipo { get; set; }

        // Clave foránea hacia Mision
        public int? MisionID { get; set; }
        public Mision? Mision { get; set; }

        public Equipo()
        {
            Descripcion = string.Empty;
        }
    }
}