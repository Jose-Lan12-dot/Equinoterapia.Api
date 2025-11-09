using System.ComponentModel.DataAnnotations;

namespace FRONTproyectoEquinoterapia.Dtos
{
    public class AddSesionDto
    {
        [Required]
        public DateOnly Fecha { get; set; }

        [Required]
        public TimeOnly Hora { get; set; }

        [Required]
        public int InstructorId { get; set; }

        [Required]
        public int CaballoId { get; set; }
        public List<int>? PacienteIds { get; set; }
    }
}