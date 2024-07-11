namespace ByteStormBackend.Models
{
    public class Equipo
    {
        public int ID { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int MisionID { get; set; }
        public Mision Mision { get; set; } = new Mision(); // Inicialización por defecto
    }
}