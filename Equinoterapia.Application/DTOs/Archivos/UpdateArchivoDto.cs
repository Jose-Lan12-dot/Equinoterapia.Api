using Microsoft.AspNetCore.Http;
namespace Equinoterapia.Application.DTOs.Archivos
{
    public class UpdateArchivoDto
    {
        public string? Nombre { get; set; }
        public byte[]? Archivo { get; set; }
    }
}
