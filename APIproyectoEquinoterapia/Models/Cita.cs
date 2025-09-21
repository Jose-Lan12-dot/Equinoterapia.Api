using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public partial class Cita
{
    public int CitaId { get; set; }

    public int SesionId { get; set; }

    public int PacienteId { get; set; }

    public bool? Asistio { get; set; }

    public virtual Paciente Paciente { get; set; } = null!;

    public virtual Sesion Sesion { get; set; } = null!;
}
