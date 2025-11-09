using System.ComponentModel.DataAnnotations;

namespace Equinoterapia.Application.DTOs
{
    public class SesionDto
    {
        [Required]
        public int SesionId { get; set; }
        [Required]
        public required DateOnly Fecha { get; set; }
        [Required]
        public required TimeOnly Hora { get; set; }
        [Required]
        public required int InstructorId { get; set; }
        [Required]
        public required int CaballoId { get; set; }
    }

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