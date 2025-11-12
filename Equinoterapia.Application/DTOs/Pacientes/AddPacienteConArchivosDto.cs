using Equinoterapia.Application.DTOs.Archivos;

namespace Equinoterapia.Application.DTOs.Pacientes
{
    public class AddPacienteConArchivosDto
    {
        public AddPacienteDto Paciente { get; set; } = new();
        public List<AddArchivoDto>? Archivos { get; set; }
    }
}
