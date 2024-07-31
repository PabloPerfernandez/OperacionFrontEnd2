namespace ByteStormBackend.Models
{
    public class Operativo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public int MisionID { get; set; }

        public Operativo()
        {
            ID = 0;
            Nombre = string.Empty;
            Rol = string.Empty;
            MisionID = 0;
        }
    }
}