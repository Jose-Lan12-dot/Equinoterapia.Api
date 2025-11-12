using Equinoterapia.Application.DTOs.Pacientes;
using Equinoterapia.Application.DTOs.Shared;

namespace Equinoterapia.Application.Interfaces
{
    public interface IPacienteQueryRepository
    {
        Task<List<GetAllPacienteDto>> GetAllAsync();
        Task<List<DropdownItemDto>> GetActivosAsync();
    }
}