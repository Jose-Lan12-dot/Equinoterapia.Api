public class PacienteDTO
{
    public int PacienteId { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public string Tutor { get; set; }
    public string? TelefonoTutor { get; set; }
    public int? Faltas { get; set; }
    public DateTime? FechaRegistro { get; set; }
}