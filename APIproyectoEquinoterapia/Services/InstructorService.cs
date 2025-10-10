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

        public async Task<(IEnumerable<InstructorDTO>, string)> GetInstructoresAsync()
        {
            var instructores = await _context.instructor
                .Select(i => new InstructorDTO
                {
                    InstructorId = i.InstructorId,
                    Nombre = i.Nombre,
                    Telefono = i.Telefono,
                    Estado = i.Estado.ToString()
                })
                .ToListAsync();

            if (instructores == null || !instructores.Any())
            {
                var mensaje = "No se encontraron instructores";
                return (Enumerable.Empty<InstructorDTO>(),mensaje );
            }

            return (instructores, "Instructores devueltos con éxito");
        }

        public async Task<(InstructorDTO?, string)> GetInstructorAsync(int instructorId)
        {
            var instructor = await _context.instructor.FindAsync(instructorId);
            if (instructor == null)
            {
                var mensaje = "Instructor no encontrado";
                return (null, mensaje);
            }

            return (new InstructorDTO
            {
                InstructorId = instructor.InstructorId,
                Nombre = instructor.Nombre,
                Telefono = instructor.Telefono,
                Estado = instructor.Estado.ToString()
            }, "Instructor encontrado");
        }

        public async Task<(bool, string)> PostInstructorAsync(InstructorDTO instructorDTO)
        {
            if (instructorDTO == null)
            {
                var mensaje = "Datos del instructor no proporcionados";
                return (false, mensaje);
            }

            if (_context.instructor.Any(i => i.InstructorId == instructorDTO.InstructorId))
            {
                var mensaje = "El Instructor ya existe.";
                return (false,mensaje);
            }

            if (!Enum.TryParse<EstadoInstructor>(instructorDTO.Estado, out var estadoValido))
            {
                var mensaje = "Estado no válido";
                return (false, mensaje);
            }

            var nuevoInstructor = new Instructor
            {
                Nombre = instructorDTO.Nombre ?? string.Empty,
                Telefono = instructorDTO.Telefono,
                Estado = estadoValido
            };

            _context.instructor.Add(nuevoInstructor);
            await _context.SaveChangesAsync();
            return (true, "Instructor agregado correctamente");
        }

        public async Task<(bool, string)> PutInstructorAsync(int instructorId, InstructorDTO instructorDTO)
        {
            var instructor = await _context.instructor.FindAsync(instructorId);
            if (instructor == null)
            {
                var mensaje = "Instructor no encontrado";
                return (false, mensaje);
            }

            if (!Enum.TryParse<EstadoInstructor>(instructorDTO.Estado, out var estadoValido))
            {
                var mensaje = "Estado no válido";
                return (false, mensaje);
            }

            instructor.Nombre = instructorDTO.Nombre ?? instructor.Nombre;
            instructor.Telefono = instructorDTO.Telefono ?? instructor.Telefono;
            instructor.Estado = estadoValido;

            await _context.SaveChangesAsync();
            return (true, "Instructor actualizado correctamente");
        }
    }
}