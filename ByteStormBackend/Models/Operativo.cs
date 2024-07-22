namespace ByteStormBackend.Models
{
    public class Operativo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public List<Mision> Misiones { get; set; }

        public Operativo()
        {
            ID = 0;
            Nombre = string.Empty;
            Rol = string.Empty;
            Misiones = new List<Mision>();
        }
    }
}