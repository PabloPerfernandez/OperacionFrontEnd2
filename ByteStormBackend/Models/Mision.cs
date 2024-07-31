using System.ComponentModel.DataAnnotations;

namespace ByteStormBackend.Models
{
    public class Mision
    {
        [Key]
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public int EquipoID { get; set; }

        public Mision()
        {
            Codigo = 0;
            Descripcion = string.Empty;
            Estado = 0;
            EquipoID = 0;
        }
    }
}