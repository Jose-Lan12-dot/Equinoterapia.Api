using Microsoft.AspNetCore.Http;
namespace Equinoterapia.Application.DTOs.Archivos
{
    public class AddArchivoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public byte[] Archivo { get; set; } = null!;

    }
}