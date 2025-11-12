using Microsoft.AspNetCore.Http;
namespace Equinoterapia.Application.DTOs.Archivos
{
    public class AddArchivoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public IFormFile Archivo { get; set; } = null!; 
    }
}