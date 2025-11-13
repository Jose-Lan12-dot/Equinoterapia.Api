namespace Equinoterapia.Application.DTOs.Sesiones
{
    public class AddSesionDto
    {

        public DateOnly Fecha { get; set; }

        public TimeOnly Hora { get; set; }
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public List<int> PacienteIds { get; set; } = new();
    }
}
