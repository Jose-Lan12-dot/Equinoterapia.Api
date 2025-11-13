namespace Equinoterapia.Application.DTOs.Sesiones
{
    public class GetAllSesionDto
    {
        public int SesionId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
    }
}
