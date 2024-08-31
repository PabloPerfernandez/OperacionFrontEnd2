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

        // Relación con Misión
        public ICollection<Mision> Misiones { get; set; } = new List<Mision>();

        // Propiedad para almacenar los códigos de misiones asignadas
        public string MisionAsignada { get; set; } = "Sin Misiones Asignadas";

        // Método para actualizar 'MisionAsignada' cada vez que cambien las misiones
        public void ActualizarMisionesAsignadas()
        {
            if (Misiones != null && Misiones.Any())
            {
                // Retornar los códigos de misiones asignadas separados por coma
                MisionAsignada = string.Join(", ", Misiones.Select(m => m.Codigo));
            }
            else
            {
                MisionAsignada = "Sin Misiones Asignadas";
            }
        }
    }
}