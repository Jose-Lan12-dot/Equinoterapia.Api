namespace Equinoterapia.Application.DTOs.Informes
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
}