namespace ByteStormBackend.Models
{
    public class Equipo
    {
        public int EquipoID { get; set; }
        public int Tipo { get; set; }
        public string Descripcion { get; set; }
        public int EstadoEquipo { get; set; }

        public Equipo()
        {
            EquipoID = 0;
            Tipo = 0;
            Descripcion = string.Empty;
            EstadoEquipo = 0;
        }
    }
}