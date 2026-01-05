namespace FRONTproyectoEquinoterapia.Models
{
    public class RespuestaLoginDto
    {
        public string Token { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public byte[]? FotoPerfil { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }


    public class LoginDataDto
    {
        public string Mensaje { get; set; } = string.Empty;
    }
    public class PerfilResponseDto
    {
        public string? NombreUsuario { get; set; }
        public string? Foto { get; set; }
    }


}





