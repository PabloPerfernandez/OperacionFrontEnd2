using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ByteStormBackend.Models
{
    public class Operativo
    {
        [Key]
        public int ID { get; set; }

        public required string Nombre { get; set; }
        public required string Rol { get; set; }

        public ICollection<Mision> Misiones { get; set; } = new List<Mision>();

        public string MisionAsignada { get; set; } = "Sin Misiones Asignadas";

        public void ActualizarMisionesAsignadas()
        {
            if (Misiones != null && Misiones.Any())
            {
                MisionAsignada = string.Join(", ", Misiones.Select(m => m.Codigo));
            }
            else
            {
                MisionAsignada = "Sin Misiones Asignadas";
            }
        }
    }
}