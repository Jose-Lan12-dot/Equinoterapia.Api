using System.ComponentModel.DataAnnotations;

namespace FRONTproyectoEquinoterapia.Models
{
    public class AddSesionDto
    {
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public List<int> PacienteIds { get; set; } = new();
    }

    public class SesionDto
    {
        public int SesionId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public string InstructorNombre { get; set; } = string.Empty;
        public string CaballoNombre { get; set; } = string.Empty;
        public List<int> PacienteIds { get; set; } = new();
        public string ColorInstructor { get; set; } = "#378006";

        // Esta es la lista de pacientes en la sesión
        public List<PacienteDto> Pacientes { get; set; } = new List<PacienteDto>();
    }

}
