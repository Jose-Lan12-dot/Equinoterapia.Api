namespace Equinoterapia.Application.DTOs.Caballos
{
    public class GetCaballoDto
    {
        public int CaballoId { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateOnly FechaHerraje { get; set; }
        public List<HistorialMedicoDto> Padecimientos { get; set; } = null!;
    }
}
