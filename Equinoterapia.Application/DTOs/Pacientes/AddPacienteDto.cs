namespace Equinoterapia.Application.DTOs.Pacientes
{
    public class AddPacienteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public string Tutor { get; set; } = string.Empty;
        public string TelefonoTutor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
    }
}
