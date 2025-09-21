using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? Telefono { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
