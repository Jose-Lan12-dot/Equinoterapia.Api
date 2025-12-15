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
    public class Paciente
    {
        public int pacienteId { get; set; }
        public string nombre { get; set; } = "";
        public DateTime? fechaNacimiento { get; set; }
        public string tutor { get; set; } = "";
        public string? telefonoTutor { get; set; }
        public string Estado { get; set; } = "Activo";
        public string? genero { get; set; }
        public DateTime fechaRegistro { get; set; } = DateTime.Now;
        public int numeroFaltas { get; set; } = 0;
        public bool formularioHecho { get; set; } = false;

    }

    public class AddPacienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; } = string.Empty;
        public string TelefonoTutor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
    }

    public class AddPacienteConArchivosDto
    {
        public AddPacienteDto Paciente { get; set; } = new();
        public List<AddArchivoBlazorDto>? Archivos { get; set; }
    }


}
