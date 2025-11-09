using Microsoft.AspNetCore.Http;
namespace Equinoterapia.Application.DTOs
{
    public class ArchivoDto
    {
        public String? Nombre { get; set; }
        public IFormFile? Archivo { get; set; }
    }
}
