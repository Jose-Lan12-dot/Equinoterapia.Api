using System;
using System.Text.Json.Serialization;

public class Paciente
{
    public int pacienteId { get; set; }
    public string nombre { get; set; } = "";
    public DateTime? fechaNacimiento { get; set; }
    public string tutor { get; set; } = "";
    public string? telefonoTutor { get; set; }
    public string Estado { get; set; } = "Activo";
    public string? genero { get; set; }
    public DateTime fechaRegistro { get; set; } = DateTime.Now;
    public int numeroFaltas { get; set; } = 0;
    public bool formularioHecho { get; set; } = false;
    
}


