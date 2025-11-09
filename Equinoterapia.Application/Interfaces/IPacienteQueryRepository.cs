using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IPacienteQueryRepository
    {
        Task<List<AllPacienteDto>> GetAllAsync();
        Task<List<DropdownItemDto>> GetActivosAsync();
    }
}