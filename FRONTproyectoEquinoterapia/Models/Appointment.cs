namespace FRONTproyectoEquinoterapia.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ColorInstructor { get; set; }

        // metadata para frontend
        public int? SesionId { get; set; }
        public int? InstructorId { get; set; }
        public int? CaballoId { get; set; }
        public List<int> PacienteIds { get; set; } = new();

        // nombre para mostrar
        public string? InstructorNombre { get; set; }
    }

}

