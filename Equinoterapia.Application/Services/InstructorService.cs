using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IInstructorQueryRepository _instructorQueryRepository;

        private readonly ISesionRepository _sesionRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;



        public InstructorService(IInstructorRepository instructorRepository, 
            IInstructorQueryRepository instructorQueryRepository,

            ISesionRepository sesionRepository,
            IAsistenciaRepository asistenciaRepository)
        {
            _instructorRepository = instructorRepository;
            _instructorQueryRepository = instructorQueryRepository;

            _sesionRepository = sesionRepository;
            _asistenciaRepository = asistenciaRepository;

        }

        public async Task<(IEnumerable<GetInstructorDto>, string)> GetAllAsync()
        {
            var instructores = new List<GetInstructorDto>();
            try
            {
                instructores = await _instructorQueryRepository.GetAllDtosAsync();
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<GetInstructorDto>(), $"Error al obtener los instructores: {ex.Message}");
            }

            if (!instructores.Any())
            {
                return (Enumerable.Empty<GetInstructorDto>(), "No se encontraron instructores");
            }

            return (instructores, "Instructores devueltos con éxito");
        }

        public async Task<(GetInstructorDto?, string)> GetByIdAsync(int instructorId)
        {
            Instructor? instructor;
            try
            {
                instructor = await _instructorRepository.GetByIdAsync(instructorId);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener el instructor: {ex.Message}");
            }

            if (IsNull(instructor))
            {
                return (null, "Instructor no encontrado");
            }

            return (MapToInstructorDto(instructor!), "Instructor encontrado");
        }

        public async Task<(bool, string)> AddAsync(AddInstructorDto addInstructorDto)
        {

            var nuevoInstructor = new Instructor
            {
                Nombre = addInstructorDto.Nombre,
                Telefono = addInstructorDto.Telefono,
                Estado = EstadoInstructor.Activo
            };

            try
            {
                _instructorRepository.Add(nuevoInstructor);
                await _instructorRepository.SaveChangesAsync();
                return (true, "Instructor agregado correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar el instructor: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdateAsync(UpdateInstructorDto instructorDto, int instructorId)
        {

            var instructor = await _instructorRepository.GetByIdAsync(instructorId);
            if (IsNull(instructor))
            {
                return (false, "Instructor no encontrado");
            }

            if (!IsEmpty(instructorDto.Telefono))
            {
                instructor.Telefono = instructorDto.Telefono!;
            }
            if (!IsEmpty(instructorDto.Nombre))
            {
                instructor.Nombre = instructorDto.Nombre!;
            }
            if (!IsEmpty(instructorDto.Estado))
            {
                instructor.Estado = GetEstado(instructorDto.Estado!, instructor.Estado);
            }

            try
            {
                await _instructorRepository.SaveChangesAsync();
                return (true, "Instructor actualizado correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el instructor: {ex.Message}");
            }
        }

        public async Task<(bool, string)> DeleteAsync(int instructorId)
        {
            var instructor = await _instructorRepository.GetByIdAsync(instructorId);
            if (IsNull(instructor))
            {
                return (false, "Instructor no encontrado");
            }

            try
            {
                var sesiones = await _sesionRepository.GetByInstructorIdAsync(instructorId);
                if (sesiones.Any())
                {
                    var sesionIds = sesiones.Select(s => s.SesionId).ToList();
                    var asistencias = await _asistenciaRepository.GetBySesionIdsAsync(sesionIds);

                    if (asistencias.Any())
                    {
                        _asistenciaRepository.RemoveRange(asistencias);
                    }

                    _sesionRepository.RemoveRange(sesiones);
                }

                _instructorRepository.Delete(instructor);

                await _instructorRepository.SaveChangesAsync();

                return (true, "Instructor y sus sesiones/asistencias asociadas eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el instructor: {ex.Message}");
            }
        }





        // --- MÉTODOS PRIVADOS (Helper) ---

        private GetInstructorDto MapToInstructorDto(Instructor instructor)
        {
            return new GetInstructorDto
            {
                InstructorId = instructor.InstructorId,
                Nombre = instructor.Nombre,
                Telefono = instructor.Telefono,
                Estado = instructor.Estado.ToString()
            };
        }

        private EstadoInstructor GetEstado(string estado, EstadoInstructor estadoActual)
        {
            if (Enum.TryParse<EstadoInstructor>(estado, out var estadoNuevo))
                return estadoNuevo;
            return estadoActual;
        }

        private bool IsNull(object? objeto) 
        {
            if (objeto == null)
                return true;
            return false;
        }

        private bool IsEmpty(string? str) => string.IsNullOrWhiteSpace(str);
    }
}