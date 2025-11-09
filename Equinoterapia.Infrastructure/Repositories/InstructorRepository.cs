using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository, IInstructorQueryRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Instructor?> GetByIdAsync(int instructorId)
        {
            return await _context.instructor.FindAsync(instructorId);
        }

        public void Add(Instructor instructor)
        {
            _context.instructor.Add(instructor);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<InstructorDto>> GetAllDtosAsync()
        {
            return await _context.instructor
                .AsNoTracking()
                .Select(i => new InstructorDto
                {
                    InstructorId = i.InstructorId,
                    Nombre = i.Nombre,
                    Telefono = i.Telefono,
                    Estado = i.Estado.ToString()
                })
                .ToListAsync();
        }

        public void Delete(Instructor instructor)
        {
            _context.instructor.Remove(instructor);
        }

        public async Task<List<DropdownItemDto>> GetActivosAsync()
        {
            return await _context.instructor
                .Where(i => i.Estado == EstadoInstructor.Activo)
                .Select(i => new DropdownItemDto
                {
                    Id = i.InstructorId,
                    Nombre = i.Nombre
                })
                .ToListAsync();
        }
    }
}