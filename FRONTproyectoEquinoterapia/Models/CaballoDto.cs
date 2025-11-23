namespace FRONTproyectoEquinoterapia.Models
{
    public class CaballoDto
    {
        public int CaballoId { get; set; }
        public string? Placa { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaHerraje { get; set; }
        public string? Padecimiento { get; set; }
    }

    public class AddCaballoDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaHerraje { get; set; }
        public string Padecimiento { get; set; } = string.Empty;
    }
}

