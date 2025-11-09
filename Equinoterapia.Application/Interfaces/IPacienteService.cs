using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<(IEnumerable<AllPacienteDto>, string)> GetAllAsync();

        Task<(PacienteDto?, int, bool, List<string>?, string)> GetByIdAsync(int pacienteId);

        Task<(bool, string)> AddAsync(AddPacienteConArchivosDto addPaciente);

        Task<(bool, string)> UpdateAllAsync(PacienteDto pacienteDto, int pacienteId);

        Task<(bool, string)> UpdatePartialAsync(PacienteDto pacienteDto, int pacienteId);

        Task<(bool, string)> DeleteAsync(int pacienteId);

    }
}