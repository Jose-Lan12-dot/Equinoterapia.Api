using System.ComponentModel.DataAnnotations;

namespace Equinoterapia.Application.DTOs.Auth
{
    public class InicioSesionDto
    {
        public int IdUsuario { get; set; }
        public int IdKronos { get; set; }
        public string? Usuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Mensaje { get; set; }
        public string? Foto { get; set; }
        public int IdPerfil { get; set; }
        public string? Perfil { get; set; }
    }


    public class InicioSesionDataDto
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string contrasena { get; set; } = string.Empty;
    }
}
