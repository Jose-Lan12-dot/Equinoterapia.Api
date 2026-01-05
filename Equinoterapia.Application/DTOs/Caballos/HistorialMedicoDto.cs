
namespace Equinoterapia.Application.DTOs.Caballos
{
    public class HistorialMedicoDto
    {
        public int HistorialId { get; set; }
        public int PadecimientoId { get; set; }
        public string NombrePadecimiento { get; set; } = null!;
        public string? Observaciones { get; set; }
    }
}
