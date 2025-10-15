using APIproyectoEquinoterapia.DTOs;

public class PacienteDTO
{
    public int? PacienteId { get; set; }
    public string? Nombre { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Tutor { get; set; }
    public string? TelefonoTutor { get; set; }
    public string? Estado { get; set; }
    public string? Genero { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public int? NumeroFaltas { get; set; }
    public bool? FormularioHecho { get; set; }
}