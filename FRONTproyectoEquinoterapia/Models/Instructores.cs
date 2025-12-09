namespace FRONTproyectoEquinoterapia.Models
{
    public class AddInstructorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }

    public class InstructorDto
    {
        public int InstructorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

    public class UpdateInstructorDto
    {
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
    }




}
