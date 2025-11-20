//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class InstructorService
//    {
//        private readonly AppDbContext _context;

//        public InstructorService(AppDbContext context)
//        {
//            _context = context;
//        }

////////////////////////
//        public async Task<(IEnumerable<UpdateInstructorDto>, string)> GetAllAsync()
//        {
//            var instructores = new List<UpdateInstructorDto>();
//            try
//            {
//                instructores = await GetAllInstructores();
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al obtener los instructores: {ex.Message}";
//                return (Enumerable.Empty<UpdateInstructorDto>(), mensaje);
//            }

//            if (instructores.Count == 0)
//            {
//                var mensaje = "No se encontraron instructores";
//                return (Enumerable.Empty<UpdateInstructorDto>(), mensaje);
//            }

//            return (instructores, "Instructores devueltos con éxito");
//        }

////////////////////////
//        public async Task<(UpdateInstructorDto?, string)> GetByIdAsync(int instructorId)
//        {
//            var instructor = null as Instructor;
//            try
//            {
//                instructor = await GetInstructorById(instructorId);
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al obtener el instructor: {ex.Message}";
//                return (null, mensaje);
//            }

//            if (IsNull(instructor))
//            {
//                var mensaje = "Instructor no encontrado";
//                return (null, mensaje);
//            }

//            return (MapToInstructorDto(instructor!), "Instructor encontrado");
//        }

////////////////////////
//        public async Task<(bool, string)> AddAsync(AddInstructorDto addInstructorDto)
//        {
//            if (ValidateInstructorData(addInstructorDto))
//            {
//                var mensaje = "Datos del instructor incompletos";
//                return (false, mensaje);
//            }

//            var nuevoInstructor = new Instructor
//            {
//                Nombre = addInstructorDto.Nombre,
//                Telefono = addInstructorDto.Telefono,
//                Estado = EstadoInstructor.Activo
//            };

//            try
//            {
//                _context.instructor.AddInforme(nuevoInstructor);
//                await _context.SaveChangesAsync();
//                return (true, "Instructor agregado correctamente");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al agregar el instructor: {ex.Message}";
//                return (false, mensaje);
//            }
//        }

////////////////////////
//        public async Task<(bool, string)> UpdateAllAsync(UpdateInstructorDto instructorDto, int instructorId)
//        {
           
//            if(instructorDto.InstructorId != instructorId)
//            {
//                var mensaje = "El ID del instructor no coincide";
//                return (false, mensaje);
//            }

//            if (ValidateInstructorData(instructorDto))
//            {
//                var mensaje = "Datos del instructor incompletos";
//                return (false, mensaje);
//            }

//            var instructor = await GetInstructorById(instructorId);
//            if (IsNull(instructor))
//            {
//                var mensaje = "Instructor no encontrado";
//                return (false, mensaje);
//            }


//            instructor!.Nombre = instructorDto.Nombre!;
//            instructor.Telefono = instructorDto.Telefono!;
//            instructor.Estado = GetEstado(instructorDto.Estado!,instructor.Estado);

//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Instructor actualizado correctamente");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al actualizar el instructor: {ex.Message}";
//                return (false, mensaje);
//            }
//        }

////////////////////////
//        public async Task<(bool, string)> UpdateAsync(UpdateInstructorDto instructorDto, int instructorId)
//        {
//            if (instructorDto.InstructorId != instructorId)
//            {
//                var mensaje = "El ID del instructor no coincide";
//                return (false, mensaje);
//            }

//            var instructor = await GetInstructorById(instructorId);
//            if (IsNull(instructor))
//            {
//                var mensaje = "Instructor no encontrado";
//                return (false, mensaje);
//            }


//            if (!IsEmpty(instructorDto.Telefono))
//            {
//                instructor!.Telefono = instructorDto.Telefono!;
//            }
//            if (!IsEmpty(instructorDto.Nombre))
//            {
//                instructor!.Nombre = instructorDto.Nombre!;
//            }
//            if (!IsEmpty(instructorDto.Estado))
//            {
//                instructor!.Estado = GetEstado(instructorDto.Estado!,instructor.Estado);
//            }


//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Instructor actualizado correctamente");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al actualizar el instructor: {ex.Message}";
//                return (false, mensaje);
//            }
//        }

////////////////////////
//        public async Task<(bool, string)> DeleteAsync(int instructorId)
//        {
//            var instructor = await GetInstructorById(instructorId);
//            if (IsNull(instructor))
//            {
//                var mensaje = "Instructor no encontrado";
//                return (false, mensaje);
//            }

//            var tieneSesiones = await InstructorHasSesiones(instructorId);
//            if (tieneSesiones)
//            {
//                var mensaje = "No se puede eliminar el instructor porque tiene sesiones asociadas.";
//                return (false, mensaje);
//            }

//            try
//            {
//                _context.instructor.Remove(instructor!);
//                await _context.SaveChangesAsync();
//                return (true, "Instructor eliminado correctamente");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al eliminar el instructor: {ex.Message}";
//                return (false, mensaje);
//            }
//        }







////////////////////////
//        private async Task<List<UpdateInstructorDto>> GetAllInstructores()
//        {
//            return await _context.instructor
//                    .Select(i => new UpdateInstructorDto
//                    {
//                        InstructorId = i.InstructorId,
//                        Nombre = i.Nombre,
//                        Telefono = i.Telefono,
//                        Estado = i.Estado.ToString()
//                    })
//                    .ToListAsync();
//        }

////////////////////////
//        private async Task<Instructor?> GetInstructorById(int instructorId)
//        {
//            return await _context.instructor.FindAsync(instructorId);
//        }

////////////////////////
//        private async Task<bool> InstructorHasSesiones(int instructorId)
//        {
//            return await _context.sesion.AnyAsync(s => s.InstructorId == instructorId);
//        }




////////////////////////
//        private UpdateInstructorDto MapToInstructorDto(Instructor instructor)
//        {
//            return new UpdateInstructorDto
//            {
//                InstructorId = instructor.InstructorId,
//                Nombre = instructor.Nombre,
//                Telefono = instructor.Telefono,
//                Estado = instructor.Estado.ToString()
//            };
//        }

////////////////////////
//        private bool ValidateInstructorData(AddInstructorDto addInstructorDto)
//        {
//            if (IsEmpty(addInstructorDto.Telefono)
//                || IsEmpty(addInstructorDto.Nombre))
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private bool ValidateInstructorData(UpdateInstructorDto instructorDto)
//        {
//            if (IsEmpty(instructorDto.Telefono)
//                || IsEmpty(instructorDto.Nombre)
//                || IsEmpty(instructorDto.Estado))
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private EstadoInstructor GetEstado(string estado, EstadoInstructor estadoActual)
//        {
//            if(Enum.TryParse<EstadoInstructor>(estado, out var estadoNuevo))
//                return estadoNuevo;
//            return estadoActual;
//        }

////////////////////////
//        private bool IsNull(object? objeto)
//        {
//            if (objeto == null)
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private bool IsEmpty(string? str)
//        {
//            return string.IsNullOrWhiteSpace(str);
//        }


//    }
//}