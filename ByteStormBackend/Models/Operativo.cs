using System.Collections.Generic;

namespace ByteStormBackend.Models
{
    public class Operativo
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public ICollection<Mision> Misiones { get; set; } = new List<Mision>();
    }
}