using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string Contrasena { get; set; } = null!;
}
