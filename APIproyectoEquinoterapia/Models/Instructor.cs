using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public enum EstadoInstructor
{
    Activo,
    Baja,
    EnDescanso
}

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public EstadoInstructor Estado { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
