namespace APIproyectoEquinoterapia.Models
{
    public class LoginEntrada
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int IdSistema { get; set; }
        public int IdPerfil { get; set; }
        public int Accion { get; set; }
    }
}
