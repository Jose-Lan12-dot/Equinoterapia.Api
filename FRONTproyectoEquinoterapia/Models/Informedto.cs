namespace FRONTproyectoEquinoterapia.Models
{
    public class DetalleDto
    {
        public int NumeroOrden { get; set; }
        public string Situacion { get; set; } = string.Empty;
        public string Acciones { get; set; } = string.Empty;
        public string Avances { get; set; } = string.Empty;
        public string Tramite { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }

    public class InformeDto
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public List<DetalleDto> Detalles { get; set; } = new();
    }

    public class GetEstadisticasDto
    {
        public int TotalTerapias { get; set; }
        public int NuevosIngresos { get; set; }
    }

}
