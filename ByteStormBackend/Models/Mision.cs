namespace ByteStormBackend.Models
{
    public class Mision
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int OperativoID { get; set; }
        public List<Equipo> Equipos { get; set; }

        public Mision()
        {
            Codigo = 0;
            Descripcion = string.Empty;
            Estado = string.Empty;
            OperativoID = 0;
            Equipos = new List<Equipo>();
        }
    }
}
