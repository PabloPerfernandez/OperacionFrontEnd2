using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class Mision
    {
        [Key]
        public int Codigo { get; set; }  // Primary Key

        public string? Descripcion { get; set; }
        public int Estado { get; set; }

        // Relación con Equipos (una misión puede tener múltiples equipos)
        public ICollection<Equipo>? Equipos { get; set; }

        // Clave foránea hacia Operativo
        public int OperativoID { get; set; }
        public required Operativo Operativo { get; set; }
    }
}