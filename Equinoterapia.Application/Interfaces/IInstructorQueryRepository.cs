using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Application.DTOs.Shared;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInstructorQueryRepository
    {
        Task<List<GetInstructorDto>> GetAllDtosAsync();
        Task<List<DropdownItemDto>> GetActivosAsync();
    }
}