using System;
using System.ComponentModel.DataAnnotations;

namespace FRONTproyectoEquinoterapia.Models
{
    // Models/Sesiones/AddSesionDto.cs
    public class AddSesionDto
    {
        // Si usas DateOnly en el server y tienes converters en ambos lados, usa DateOnly.
        // public DateOnly Fecha { get; set; }
        // public TimeOnly Hora { get; set; }

        // Alternativa segura (string ISO):
        public string Fecha { get; set; } = string.Empty; // "yyyy-MM-dd"
        public string Hora { get; set; } = string.Empty;  // "HH:mm" o "HH:mm:ss"
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public List<int> PacienteIds { get; set; } = new();
}

public class GetAllSesionDto
    {
        public int SesionId { get; set; }
        public string Fecha { get; set; } = string.Empty; // "yyyy-MM-dd"
        public string Hora { get; set; } = string.Empty;  // "HH:mm"
        public string nombreInstructor { get; set; } = string.Empty;
        public string nombreCaballo { get; set; } = string.Empty;
    }


    public class GetOneSesionDto
    {
        public int SesionId { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string Hora { get; set; } = string.Empty;
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public string nombreInstructor { get; set; } = string.Empty;
        public string nombreCaballo { get; set; } = string.Empty;
        public List<int>? PacienteIds { get; set; } // si el backend lo devuelve
    }

   
    public class HorarioOcupadoDto
    {
        public string HoraInicio { get; set; } = string.Empty; // "HH:mm"
        public string HoraFin { get; set; } = string.Empty;    // "HH:mm"
    }


    public class UpdateSesionDto
    {
        public string? Fecha { get; set; } // null si no se actualiza
        public string? Hora { get; set; }
        public int? InstructorId { get; set; }
        public int? CaballoId { get; set; }
        public List<int>? PacienteIds { get; set; }
    }
    public class FormDataDto
    {
        public List<SimpleDto> Instructores { get; set; } = new();
        public List<SimpleDto> Caballos { get; set; } = new();
        public List<SimpleDto> Pacientes { get; set; } = new();
    }

    public class SimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

}
