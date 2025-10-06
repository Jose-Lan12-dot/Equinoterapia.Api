public class PacienteDTO
{
    public int? PacienteId { get; set; }
    public required string Nombre { get; set; }
    public required DateOnly FechaNacimiento { get; set; }
    public required string Tutor { get; set; }
    public required string TelefonoTutor { get; set; }
    public string? Estado { get; set; }

    public string? Genero { get; set; }
    public DateTime? FechaRegistro { get; set; }
}