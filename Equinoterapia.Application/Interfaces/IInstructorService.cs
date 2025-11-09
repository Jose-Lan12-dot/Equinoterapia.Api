using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInstructorService
    {
        Task<(IEnumerable<InstructorDto>, string)> GetAllAsync();
        Task<(InstructorDto?, string)> GetByIdAsync(int instructorId);
        Task<(bool, string)> AddAsync(AddInstructorDto addInstructorDto);
        Task<(bool, string)> UpdateAllAsync(InstructorDto instructorDto, int instructorId);
        Task<(bool, string)> UpdatePartialAsync(InstructorDto instructorDto, int instructorId);

        Task<(bool, string)> DeleteAsync(int instructorId);
    }
}