using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ByteStormBackend.Models;

namespace ByteStormBackend.DTO
{
    public class CrearMisionDTO
    {
        public string? Descripcion { get; set; }
        public int Estado { get; set; }

        public ICollection<Equipo>? Equipos { get; set; }

        public int OperativoID { get; set; }
    }
}