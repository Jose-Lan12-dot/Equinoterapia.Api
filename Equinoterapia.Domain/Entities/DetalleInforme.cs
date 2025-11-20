
namespace Equinoterapia.Domain.Entities
{
    public class DetalleInforme //FILA DE LA TABLA INFORME MENSUAL
    {
        public int DetalleInformeId { get; set; }
        public int InformeMensualId { get; set; }
        public int NumeroOrden { get; set; }
        public string Situacion { get; set; } = string.Empty;
        public string Acciones { get; set; } = string.Empty;
        public string Avances { get; set; } = string.Empty;
        public string Tramite { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;


        public InformeMensual Informe { get; set; } = null!;
    }
}