namespace Equinoterapia.Application.DTOs.Asistencias
{
    public class AddAsistenciaDto
    {
        public int SesionId { get; set; }
        public List<int> PacientesId { get; set; } = new();
    }
}
