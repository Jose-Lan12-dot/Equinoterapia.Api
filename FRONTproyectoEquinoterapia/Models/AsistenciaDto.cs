using System;
using System.Text.Json.Serialization;

namespace FRONTproyectoEquinoterapia.Models
{
    public class AddAsistenciaDto
    {
        public int SesionId { get; set; }
        public List<int> PacientesId { get; set; } = new();
    }
public class GetAsistenciaDto
    {
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public int AsistenciaId { get; set; }
        public string  nombre { get; set; } = string.Empty;
        public bool Asistio { get; set; }
    }

public class UpdateAsistenciaItemDto
    {
        public int AsistenciasId { get; set; }
        public bool Asistio { get; set; }
    }

public class UpdateAsistenciaDto
    {
        public List<UpdateAsistenciaItemDto> Asistencias { get; set; } = new();
    }

}
