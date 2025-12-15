namespace FRONTproyectoEquinoterapia.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; } = string.Empty;

        // SOLO PARA EL FRONT (color de la sesión)
        public string ColorHex { get; set; } = "#eff1f6";
        // metadata para frontend
        public int? SesionId { get; set; }
        public int? InstructorId { get; set; }
        public int? CaballoId { get; set; }
        public List<int> PacienteIds { get; set; } = new();

        // nombre para mostrar
        public string? InstructorNombre { get; set; }
    }

}

