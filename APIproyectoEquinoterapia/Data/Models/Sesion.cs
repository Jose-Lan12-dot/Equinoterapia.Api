using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Data.Models;

public partial class Sesion
{
    public int SesionId { get; set; }

    public int InstructorId { get; set; }

    public int CaballoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual Caballo Caballo { get; set; } = null!;
}
