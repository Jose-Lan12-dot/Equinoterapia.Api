namespace Equinoterapia.Application.DTOs.Shared
{
    public class SesionFormDataDto
    {
        public List<DropdownItemDto> Pacientes { get; set; } = new();
        public List<DropdownItemDto> Instructores { get; set; } = new();
        public List<DropdownItemDto> Caballos { get; set; } = new();
    }
}
