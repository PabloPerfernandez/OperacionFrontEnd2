using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class Equipo
    {
        [Key]
        public int EquipoCodigo { get; set; }  // Esta es la clave primaria
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
        public int EstadoEquipo { get; set; }

        // Clave foránea hacia Mision
        public int? MisionID { get; set; }
        public Mision? Mision { get; set; }  // Navegación hacia la entidad Mision

        public Equipo()
        {
            // Inicialización de las propiedades
            Descripcion = string.Empty;  // Puedes inicializar el resto según sea necesario
        }
    }
}