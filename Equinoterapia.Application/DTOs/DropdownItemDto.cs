namespace Equinoterapia.Application.DTOs
{
    public class DropdownItemDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }


    public class SesionFormDataDto
    {
        public List<DropdownItemDto> Pacientes { get; set; } = new();
        public List<DropdownItemDto> Instructores { get; set; } = new();
        public List<DropdownItemDto> Caballos { get; set; } = new();
    }


}
