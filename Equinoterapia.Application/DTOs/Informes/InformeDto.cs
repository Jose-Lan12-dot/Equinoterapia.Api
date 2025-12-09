namespace Equinoterapia.Application.DTOs.Informes
{

    public class InformeDto
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string numeroTarjeta { get; set; } = string.Empty;
        public List<DetalleDto> Detalles { get; set; } = new();
    }
}