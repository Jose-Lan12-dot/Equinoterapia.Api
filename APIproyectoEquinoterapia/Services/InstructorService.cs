using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Services
{
    public class InstructorService
    {
        private readonly AppDbContext _context;

        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorDTO>> GetInstructoresAsync()
        {
            return await _context.instructor
                .Select(i => new InstructorDTO
                {
                    InstructorId = i.InstructorId,
                    Nombre = i.Nombre,
                    Telefono = i.Telefono
                })
                .ToListAsync();
        }

        public async Task<InstructorDTO?> GetInstructorAsync(int id)
        {
            var instructor = await _context.instructor.FindAsync(id);
            if (instructor == null)
                return null;

            return new InstructorDTO
            {
                InstructorId = instructor.InstructorId,
                Nombre = instructor.Nombre,
                Telefono = instructor.Telefono
            };
        }

        public async Task<bool> PostInstructorAsync(InstructorDTO instructorDTO)
        {
            var instructor = new Instructor
            {
                Nombre = instructorDTO.Nombre,
                Telefono = instructorDTO.Telefono
            };

            _context.instructor.Add(instructor);
            await _context.SaveChangesAsync();
            instructorDTO.InstructorId = instructor.InstructorId;
            return true;
        }

        public async Task<bool> PutInstructorAsync(int id, InstructorDTO instructorDTO)
        {
            var instructor = await _context.instructor.FindAsync(id);
            if (instructor == null)
                return false;

            instructor.Nombre = instructorDTO.Nombre;
            instructor.Telefono = instructorDTO.Telefono;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _context.instructor.FindAsync(id);
            if (instructor == null)
                return false;

            _context.instructor.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}