using System.Collections.Generic;
using System.Linq;
using ByteStormBackend.Models;

namespace ByteStormBackend.DTO
{
    public class ActualizarOperativoDTO
    {
        public required string Nombre { get; set; }
        public required string Rol { get; set; }

        public ICollection<Mision> Misiones { get; set; } = new List<Mision>();

        public string MisionAsignada { get; set; } = "Sin Misiones Asignadas";
    }
}