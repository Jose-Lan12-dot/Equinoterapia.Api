
namespace Equinoterapia.Application.DTOs.Caballos
{
    public class GetAllCaballoDto
    {
        public int CaballoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateOnly FechaHerraje { get; set; }
    }
}
