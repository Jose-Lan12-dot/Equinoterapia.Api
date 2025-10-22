using APIproyectoEquinoterapia.DTOs;

public class PacienteDto
{
    public int PacienteId { get; set; }
    public string? Nombre { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Tutor { get; set; }
    public string? TelefonoTutor { get; set; }
    public string? Estado { get; set; }
    public string? Genero { get; set; }
}

public class AllPacienteDto
{
    public int PacienteId { get; set; }
    public required string Nombre { get; set; }
    public required string Tutor { get; set; }
    public required string Estado { get; set; }
    public required int NumeroFaltas { get; set; }
    public required bool FormularioHecho { get; set; }
    public required bool FaltanArchivos { get; set; }
}


public class AddPacienteDto
{
    public required string Nombre { get; set; }
    public required DateOnly FechaNacimiento { get; set; }
    public required string Tutor { get; set; }
    public required string TelefonoTutor { get; set; }
    public required string Genero { get; set; }
}

