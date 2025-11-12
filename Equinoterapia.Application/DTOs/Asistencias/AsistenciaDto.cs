using System.ComponentModel.DataAnnotations;

namespace Equinoterapia.Application.DTOs.Asistencias
{
    public class AsistenciaDto
    {
        public int AsistenciaId { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool Asistio { get; set; }
    }

    public class AsistenciaAddDto
    {
        [Required]
        public int SesionId { get; set; }
        [Required]
        [MinLength(1)]
        public List<int> PacientesId { get; set; } = new ();
    }

    public class AsistenciaUpdateItemDto
    {
        [Required]
        public int AsistenciasId { get; set; }
        [Required]
        public bool Asistio { get; set; }
    }
    public class AsistenciaUpdateDto
    {
        [Required]
        [MinLength(1)]
        public List<AsistenciaUpdateItemDto> Asistencias { get; set; } = new ();
    }
}
