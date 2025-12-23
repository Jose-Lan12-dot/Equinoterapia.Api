using System.ComponentModel.DataAnnotations;

namespace Equinoterapia.Application.DTOs.Asistencias
{
    public class GetAsistenciaDto
    {
        public int AsistenciaId { get; set; }
        public int PacienteId { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool Asistio { get; set; }
    }

}
