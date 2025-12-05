namespace Equinoterapia.Application.DTOs.Pacientes
{
    public class UpdatePacienteDto
    {
        public int PacienteId { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Tutor { get; set; }
        public string? TelefonoTutor { get; set; }
        public string? Estado { get; set; }
        public string? Genero { get; set; }
    }
}

