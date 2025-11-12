namespace Equinoterapia.Application.DTOs.Instructores
{
    public class AddInstructorDto
    {
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
    }

    public class InstructorDto
    {
        public int InstructorId { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
    }
}








