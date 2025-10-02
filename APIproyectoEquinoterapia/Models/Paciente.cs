using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;


public enum EstadoPaciente
{
    Activo,
    Baja,
    TratamientoCompleto
}

public partial class Paciente
{
    public int PacienteId { get; set; }

    public required string Nombre { get; set; }

    public required DateOnly FechaNacimiento { get; set; }

    public required string Tutor { get; set; }

    public required string TelefonoTutor { get; set; }

    public EstadoPaciente Estado { get; set; }

    public byte[]? DiagnosticoMedico { get; set; }

    public byte[]? CertificadoMedico { get; set; }

    public byte[]? ActaNacimiento { get; set; }

    public byte[]? Curp { get; set; }

    public byte[]? ComprobanteDomicilio { get; set; }

    public byte[]? IneTutor { get; set; }

    public byte[]? Fotografia { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Formulario? Formulario { get; set; }

}

