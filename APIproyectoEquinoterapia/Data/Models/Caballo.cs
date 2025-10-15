using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Data.Models;


public enum EstadoCaballo
{
    Disponible,
    EnTratamiento,
    Descansando,
    Retirado
}


public partial class Caballo
{
    public string Placa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public EstadoCaballo Estado { get; set; }

    public DateOnly? FechaHerraje { get; set; }

    public string? Padecimiento { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
