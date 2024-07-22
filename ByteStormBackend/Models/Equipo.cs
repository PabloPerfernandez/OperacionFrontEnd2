namespace ByteStormBackend.Models
{
    public class Equipo
    {
        public bool Tipo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public Equipo()
        {
            Tipo = true;
            Descripcion = string.Empty;
            Estado = true;
        }
    }
}