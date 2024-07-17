namespace ByteStormBackend.Models
{
    public class Equipo
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int MisionID { get; set; }
        public required Mision Mision { get; set; }

        public Equipo()
        {
            Tipo = string.Empty;
            Descripcion = string.Empty;
            Estado = string.Empty;
        }
    }
}