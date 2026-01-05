//using System;
//using System.Collections.Generic;

//namespace APIproyectoEquinoterapia.Data.Models;


//public enum EstadoCaballo
//{
//    Disponible,
//    EnTratamiento,
//    Descansando,
//    Retirado
//}


//public partial class Caballo
//{
//    public int CaballoId { get; set; }
//    public required string Placa { get; set; } = null!;

//    public required string Nombre { get; set; } = null!;

//    public required EstadoCaballo Estado { get; set; }

//    public required DateOnly FechaHerraje { get; set; }

//    public required string Padecimientos { get; set; }

//    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();

//}
