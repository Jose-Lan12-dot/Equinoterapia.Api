namespace Equinoterapia.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public byte[]? FotoPerfil { get; set; } // Se serializará como Base64 automáticamente
        public string Mensaje { get; set; } = string.Empty;
    }
}