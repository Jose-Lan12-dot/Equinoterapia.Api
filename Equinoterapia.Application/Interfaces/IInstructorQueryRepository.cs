using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Application.DTOs.Shared;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInstructorQueryRepository
    {
        Task<List<InstructorDto>> GetAllDtosAsync();
        Task<List<DropdownItemDto>> GetActivosAsync();
    }
}