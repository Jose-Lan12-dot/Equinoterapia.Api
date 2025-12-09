namespace Equinoterapia.Domain.Entities
{
    public class InformeMensual
    {
        public int InformeMensualId { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string numeroTarjeta { get; set; } = string.Empty;
        public DateTime FechaModificacion { get; set; }



        public List<DetalleInforme> Detalles { get; set; } = new();
    }
}