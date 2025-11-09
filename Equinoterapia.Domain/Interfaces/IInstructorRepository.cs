using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IInstructorRepository
    {
        Task<Instructor?> GetByIdAsync(int instructorId);
        void Add(Instructor instructor);
        Task<bool> SaveChangesAsync();

        void Delete(Instructor instructor);

    }
}