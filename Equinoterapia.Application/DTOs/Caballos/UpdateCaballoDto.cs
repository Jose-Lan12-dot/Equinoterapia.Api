namespace Equinoterapia.Application.DTOs.Caballos
{
    public class UpdateCaballoDto
    {
        public string? Placa { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public DateOnly? FechaHerraje { get; set; }
        public List<HistorialMedicoDto>? Padecimientos { get; set; }
    }
}
