using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInstructorQueryRepository
    {
        Task<List<InstructorDto>> GetAllDtosAsync();
        Task<List<DropdownItemDto>> GetActivosAsync();
    }
}