//using System;
//using System.Collections.Generic;

//namespace APIproyectoEquinoterapia.Data.Models;

//public enum EstadoInstructor
//{
//    Activo,
//    Baja,
//    EnDescanso
//}

//public partial class Instructor
//{
//    public int InstructorId { get; set; }

//    public required string Nombre { get; set; } = null!;

//    public required string Telefono { get; set; }

//    public required EstadoInstructor Estado { get; set; }
//    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
//}
