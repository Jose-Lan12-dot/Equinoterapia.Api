using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public partial class Paciente
{
    public int PacienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Tutor { get; set; } = null!;

    public string? TelefonoTutor { get; set; }


    public byte[]? DiagnosticoMedico { get; set; }

    public byte[]? CertificadoMedico { get; set; }

    public byte[]? ActaNacimiento { get; set; }

    public byte[]? Curp { get; set; }

    public byte[]? ComprobanteDomicilio { get; set; }

    public byte[]? IneTutor { get; set; }

    public byte[]? Fotografia { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
