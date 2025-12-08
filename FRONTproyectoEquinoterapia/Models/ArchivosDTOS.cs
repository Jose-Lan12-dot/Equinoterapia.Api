// CORRECTO
using Microsoft.AspNetCore.Components.Forms;

namespace FRONTproyectoEquinoterapia.Models
{
    public class AddArchivoBlazorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public byte[] Archivo { get; set; } = null!;
    }



    public class UpdateArchivoBlazorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public byte[] Archivo { get; set; } = null!;
    }

    public class ArchivoPacienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int TipoArchivoId { get; set; }
        public string Extension { get; set; } = string.Empty;
    }
}

