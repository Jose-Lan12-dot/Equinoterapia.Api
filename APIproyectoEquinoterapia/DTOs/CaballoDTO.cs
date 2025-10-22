namespace APIproyectoEquinoterapia.DTOs
{
    public class AddCaballoDto
    {
        public required string Placa { get; set; }
        public required string Nombre { get; set; }
        public required DateOnly FechaHerraje { get; set; }
        public required string Padecimiento { get; set; }
    }


    public class CaballoDto
    {
        public int CaballoId { get; set; }
        public string? Placa { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public DateOnly? FechaHerraje { get; set; }
        public string? Padecimiento { get; set; }
    }



}
