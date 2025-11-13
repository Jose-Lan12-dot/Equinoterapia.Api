
namespace Equinoterapia.Application.DTOs.Instructores
{
    public class GetInstructorDto
    {
        public int InstructorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
