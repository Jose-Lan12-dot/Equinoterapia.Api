namespace APIproyectoEquinoterapia.DTOs
{
    public class CaballoDTO
    {
        public string Placa { get; set; }
        public string Nombre { get; set; }
        public string? Estado { get; set; }
        public DateOnly? FechaHerraje { get; set; }
        public string? Padecimiento { get; set; }
    }
}
