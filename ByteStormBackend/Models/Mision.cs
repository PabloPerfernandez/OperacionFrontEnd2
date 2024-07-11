namespace ByteStormBackend.Models
{
    public class Mision
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int OperativoID { get; set; }
        public Operativo Operativo { get; set; }
        public List<Equipo> Equipos { get; set; }
    }
}