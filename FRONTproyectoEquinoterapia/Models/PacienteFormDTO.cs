using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace FRONTproyectoEquinoterapia.Models
{
    public class PacienteFormDTO
    {
        public int PacienteId { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string Tutor { get; set; } = string.Empty;
        public string? TelefonoTutor { get; set; }
        public string Estado { get; set; } = "Activo";
        public string Genero { get; set; } = "Sin seleccionar";
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int NumeroFaltas { get; set; } = 0;
        public bool FormularioHecho { get; set; } = true;

        public ArchivoDTO Archivo { get; set; } = new ArchivoDTO();
    }
}
