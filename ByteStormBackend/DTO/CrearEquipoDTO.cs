using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class CrearEquipoDTO
    {
        public int equipoTipo { get; set; }
        public string? equipoDescripcion { get; set; }
        public int EstadoEquipo { get; set; }
        public int? MisionID { get; set; }
    }
}