namespace Equinoterapia.Application.DTOs.Sesiones
{
    public class GetAllSesionDto
    {
        public int SesionId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }

        
       
        public string nombreInstructor { get; set; } = string.Empty;
        public string nombreCaballo { get; set; } = string.Empty;
    }


}
