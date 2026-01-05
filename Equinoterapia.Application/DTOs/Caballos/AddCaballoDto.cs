
namespace Equinoterapia.Application.DTOs.Caballos
{
    public class AddCaballoDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateOnly FechaHerraje { get; set; }
        public List<HistorialMedicoDto> Padecimientos { get; set;} = new();
    }

}
