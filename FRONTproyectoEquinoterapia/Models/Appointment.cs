namespace FRONTproyectoEquinoterapia.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ColorInstructor { get; set; }

        // metadata que necesitamos en frontend
        public int? SesionId { get; set; }
        public int? InstructorId { get; set; }
        public int? CaballoId { get; set; }
        public List<int>? PacienteIds { get; set; }

        // nombre para mostrar (llenado en frontend)
        public string? InstructorNombre { get; set; }
    }


}

