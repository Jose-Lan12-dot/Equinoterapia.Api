using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRONTproyectoEquinoterapia.Models
{
    public class PacienteDto
    {
        public int PacienteId { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public DateOnly? FechaNacimiento { get; set; }
        public string? Tutor { get; set; }
        public string? TelefonoTutor { get; set; }
        public string? Estado { get; set; }
        public string? Genero { get; set; }
    }

    public class AllPacienteDto
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tutor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int NumeroFaltas { get; set; }
        public bool FormularioHecho { get; set; }
        public bool FaltanArchivos { get; set; }
    }

    public class AddPacienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public string Tutor { get; set; } = string.Empty;
        public string TelefonoTutor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
    }

    public class AddPacienteConArchivosDto
    {
        public AddPacienteDto Paciente { get; set; } = new();
        public List<AddArchivoBlazorDto>? Archivos { get; set; }
    }

    public class PacienteDetalleResponse
    {
        public PacienteDto? Paciente { get; set; }
        public int NumeroFaltas { get; set; } = 0;
        public bool FaltaEvaluacion { get; set; } = false;
        public List<string>? ArchivosPendientes { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }

    public class UpdatePacienteDto
    {
        public int PacienteId { get; set; }
        public string? Nombre { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Tutor { get; set; }
        public string? TelefonoTutor { get; set; }
        public string? Estado { get; set; }
        public string? Genero { get; set; }
    }

    public class ArchivoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string TipoArchivo { get; set; } = string.Empty;
        public byte[]? Contenido { get; set; }
    }
}
