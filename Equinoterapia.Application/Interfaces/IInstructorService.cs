using Equinoterapia.Application.DTOs.Instructores;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInstructorService
    {
        Task<(IEnumerable<GetInstructorDto>, string)> GetAllAsync();
        Task<(GetInstructorDto?, string)> GetByIdAsync(int instructorId);
        Task<(bool, string)> AddAsync(AddInstructorDto addInstructorDto);
        Task<(bool, string)> UpdateAsync(UpdateInstructorDto instructorDto, int instructorId);

        Task<(bool, string)> DeleteAsync(int instructorId);
    }
}