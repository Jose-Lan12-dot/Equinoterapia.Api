namespace APIproyectoEquinoterapia.Models
{
    public class LoginRespuesta
    {
        public int IdUsuario { get; set; }
        public int IdKronos { get; set; }
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Mensaje { get; set; }
        public string Foto { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
    }
}
