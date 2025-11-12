using Equinoterapia.Application.DTOs.Pacientes;

namespace Equinoterapia.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<(IEnumerable<GetAllPacienteDto>, string)> GetAllAsync();

        Task<(PacienteDetailsDto?, string)> GetByIdAsync(int pacienteId);

        Task<(bool, string)> AddAsync(AddPacienteConArchivosDto addPaciente);


        Task<(bool, string)> UpdateAsync(UpdatePacienteDto pacienteDto, int pacienteId);

        Task<(bool, string)> DeleteAsync(int pacienteId);

    }
}