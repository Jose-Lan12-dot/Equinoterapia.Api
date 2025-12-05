namespace Equinoterapia.Application.DTOs.Pacientes
{
    public class PacienteDetailsDto
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; } = string.Empty;
        public string TelefonoTutor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        public int NumeroFaltas { get; set; }
        public bool FaltaEvaluacion { get; set; }
        public List<string> ArchivosPendientes { get; set; } = new();
    }
}