namespace APIproyectoEquinoterapia.DTOs
{
    public class SesionDTO
    {
        public int SesionId { get; set; }
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
    }
}