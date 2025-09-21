using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public partial class Sesion
{
    public int SesionId { get; set; }

    public int InstructorId { get; set; }

    public string Placa { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual Caballo PlacaNavigation { get; set; } = null!;
}
