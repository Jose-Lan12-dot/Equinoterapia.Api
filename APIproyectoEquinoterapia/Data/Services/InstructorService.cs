using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIproyectoEquinoterapia.Data.Services
{
    public class InstructorService
    {
        private readonly AppDbContext _context;

        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

//////////////////////
        public async Task<(IEnumerable<InstructorDto>, string)> GetAllAsync()
        {
            var instructores = new List<InstructorDto>();
            try
            {
                instructores = await GetAllInstructores();
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al obtener los instructores: {ex.Message}";
                return (Enumerable.Empty<InstructorDto>(), mensaje);
            }

            if (instructores.Count == 0)
            {
                var mensaje = "No se encontraron instructores";
                return (Enumerable.Empty<InstructorDto>(), mensaje);
            }

            return (instructores, "Instructores devueltos con éxito");
        }

//////////////////////
        public async Task<(InstructorDto?, string)> GetByIdAsync(int instructorId)
        {
            var instructor = await GetInstructorById(instructorId);

            if (IsNull(instructor))
            {
                var mensaje = "Instructor no encontrado";
                return (null, mensaje);
            }

            return (MapToInstructorDto(instructor!), "Instructor encontrado");
        }

//////////////////////
        public async Task<(bool, string)> CreateAsync(InstructorCreationDto instructorCreationDto)
        {
            if (ValidateInstructorData(instructorCreationDto))
            {
                var mensaje = "Datos del instructor no válidos";
                return (false, mensaje);
            }

            var nuevoInstructor = new Instructor
            {
                Nombre = instructorCreationDto.Nombre,
                Telefono = instructorCreationDto.Telefono,
                Estado = EstadoInstructor.Activo
            };

            try
            {
                _context.instructor.Add(nuevoInstructor);
                await _context.SaveChangesAsync();
                return (true, "Instructor agregado correctamente");
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al agregar el instructor: {ex.Message}";
                return (false, mensaje);
            }
        }

//////////////////////
        public async Task<(bool, string)> UpdateAllAsync(InstructorDto instructorDto, int instructorId)
        {
           
            if(instructorDto.InstructorId != instructorId)
            {
                var mensaje = "El ID del instructor no coincide";
                return (false, mensaje);
            }

            if (IsEmpty(instructorDto.Telefono) 
                || IsEmpty(instructorDto.Nombre) 
                || IsEmpty(instructorDto.Estado))
            {
                var mensaje = "Datos del instructor incompletos";
                return (false, mensaje);
            }

            var instructor = await GetInstructorById(instructorId);
            if (IsNull(instructor))
            {
                var mensaje = "Instructor no encontrado";
                return (false, mensaje);
            }


            instructor!.Nombre = instructorDto.Nombre;
            instructor.Telefono = instructorDto.Telefono;
            instructor.Estado = GetEstado(instructorDto.Estado);

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Instructor actualizado correctamente");
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al actualizar el instructor: {ex.Message}";
                return (false, mensaje);
            }
        }

//////////////////////
        public async Task<(bool, string)> UpdatePartialAsync(InstructorDto instructorDto, int instructorId)
        {
            if (instructorDto.InstructorId != instructorId)
            {
                var mensaje = "El ID del instructor no coincide";
                return (false, mensaje);
            }

            var instructor = await GetInstructorById(instructorId);
            if (IsNull(instructor))
            {
                var mensaje = "Instructor no encontrado";
                return (false, mensaje);
            }


            if (!IsEmpty(instructorDto.Telefono))
            {
                instructor!.Telefono = instructorDto.Telefono;
            }
            if (!IsEmpty(instructorDto.Nombre))
            {
                instructor!.Nombre = instructorDto.Nombre;
            }
            if (!IsEmpty(instructorDto.Estado))
            {
                instructor!.Estado = GetEstado(instructorDto.Estado);
            }


            try
            {
                await _context.SaveChangesAsync();
                return (true, "Instructor actualizado correctamente");
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al actualizar el instructor: {ex.Message}";
                return (false, mensaje);
            }
        }









//////////////////////
        private async Task<List<InstructorDto>> GetAllInstructores()
        {
            return await _context.instructor
                    .Select(i => new InstructorDto
                    {
                        InstructorId = i.InstructorId,
                        Nombre = i.Nombre,
                        Telefono = i.Telefono,
                        Estado = i.Estado.ToString()
                    })
                    .ToListAsync();
        }

//////////////////////
        private async Task<Instructor?> GetInstructorById(int instructorId)
        {
            return await _context.instructor.FindAsync(instructorId);
        }

//////////////////////
        private InstructorDto MapToInstructorDto(Instructor instructor)
        {
            return new InstructorDto
            {
                InstructorId = instructor.InstructorId,
                Nombre = instructor.Nombre,
                Telefono = instructor.Telefono,
                Estado = instructor.Estado.ToString()
            };
        }

//////////////////////
        private bool ValidateInstructorData(InstructorCreationDto instructorCreationDto)
        {
            if (IsEmpty(instructorCreationDto.Telefono)
                || IsEmpty(instructorCreationDto.Nombre))
            {
                return true;
            }
            return false;
        }

//////////////////////
        private EstadoInstructor GetEstado(string estado)
        {
            Enum.TryParse<EstadoInstructor>(estado, out var estadoValido);
            return estadoValido;
        }

//////////////////////
        private bool IsNull(object? objeto)
        {
            if (objeto == null)
            {
                return true;
            }
            return false;
        }

//////////////////////
        private bool IsEmpty(string? str)
        {
            return string.IsNullOrEmpty(str);
        }


    }
}