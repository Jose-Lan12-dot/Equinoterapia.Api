using System.Text.Json.Serialization;

namespace FRONTproyectoEquinoterapia.Models
{
    public class AsistenciaDto
    {
        [JsonPropertyName("asistenciaId")]
        public int AsistenciaId { get; set; }

        // El backend usa "nombre" (minúscula). Mapéalo aquí a Nombre con JsonPropertyName
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("asistio")]
        public bool Asistio { get; set; }
    }

    public class AsistenciaAddDto
    {
        public int SesionId { get; set; }
        public List<int> PacientesId { get; set; } = new();
    }

    public class AsistenciaUpdateItemDto
    {
        public int AsistenciasId { get; set; }
        public bool Asistio { get; set; }
    }

    public class AsistenciaUpdateDto
    {
        public List<AsistenciaUpdateItemDto> Asistencias { get; set; } = new();
    }
}
