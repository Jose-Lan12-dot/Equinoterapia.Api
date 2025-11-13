namespace Equinoterapia.Application.DTOs.Sesiones
{
    public class GetOneSesionDto
    {
       public int SesionId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int InstructorId { get; set; }
        public int CaballoId { get; set; }
    }
}
