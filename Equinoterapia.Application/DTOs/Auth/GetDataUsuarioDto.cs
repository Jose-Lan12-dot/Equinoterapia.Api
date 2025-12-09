using System.ComponentModel.DataAnnotations;

namespace Equinoterapia.Application.DTOs.Auth
{
    public class GetDataUsuarioDto
    {
        public int IdUsuario { get; set; }
        public int IdKronos { get; set; }
        public string? Usuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Mensaje { get; set; }
        public byte[]? Foto { get; set; }
        public int IdPerfil { get; set; }
        public string? Perfil { get; set; }
    }
}
