// CORRECTO
using Microsoft.AspNetCore.Components.Forms;

namespace FRONTproyectoEquinoterapia.Models
{
    public class AddArchivoBlazorDto
    {

        public int TipoArchivoId { get; set; }           // <-- nuevo
        public string Nombre { get; set; } = string.Empty; // mantén por compatibilidad
        public IBrowserFile Archivo { get; set; } = null!;
    }

    public class UpdateArchivoBlazorDto
    {
        public int TipoArchivoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public IBrowserFile Archivo { get; set; } = null!;
    }
    public class ArchivoPacienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int TipoArchivoId { get; set; }
        public string Extension { get; set; } = string.Empty;
    }
}

