//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.EntityFrameworkCore;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class SesionService
//    {
//        private readonly AppDbContext _context;
//        private readonly AsistenciaService _asistenciaService;

//        public SesionService(AppDbContext context, AsistenciaService asistenciaService)
//        {
//            _context = context;
//            _asistenciaService = asistenciaService;
//        }

//        public async Task<(IEnumerable<SesionDto>, string)> GetAllAsync()
//        {
//            try
//            {
//                var sesiones = await _context.sesion
//                    .Select(s => new SesionDto
//                    {
//                        SesionId = s.SesionId,
//                        Fecha = s.Fecha,
//                        Hora = s.Hora,
//                        InstructorId = s.InstructorId,
//                        CaballoId = s.CaballoId
//                    })
//                    .ToListAsync();

//                if (sesiones.Count == 0)
//                    return (sesiones, "No se encontraron sesiones.");

//                return (sesiones, "Sesiones obtenidas correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return (Enumerable.Empty<SesionDto>(), $"Error al obtener sesiones: {ex.Message}");
//            }
//        }

//        public async Task<(SesionDto?, string)> GetByIdAsync(int sesionId)
//        {
//            try
//            {
//                var sesion = await _context.sesion.FindAsync(sesionId);
//                if (sesion == null)
//                    return (null, "Sesión no encontrada.");

//                var dto = new SesionDto
//                {
//                    SesionId = sesion.SesionId,
//                    Fecha = sesion.Fecha,
//                    Hora = sesion.Hora,
//                    InstructorId = sesion.InstructorId,
//                    CaballoId = sesion.CaballoId
//                };
//                return (dto, "Sesión encontrada.");
//            }
//            catch (Exception ex)
//            {
//                return (null, $"Error al obtener la sesión: {ex.Message}");
//            }
//        }

//        public async Task<(bool, string)> AddAsync(AddSesionDto addSesionDto)
//        {
//            var nuevaSesion = new Sesion
//            {
//                Fecha = addSesionDto.Fecha,
//                Hora = addSesionDto.Hora,
//                InstructorId = addSesionDto.InstructorId,
//                CaballoId = addSesionDto.CaballoId
//            };

//            try
//            {
//                _context.sesion.Add(nuevaSesion);
//                await _context.SaveChangesAsync();
//                if (addSesionDto.PacienteIds != null && addSesionDto.PacienteIds.Count > 0)
//                {
//                    await _asistenciaService.AddAsync(new AsistenciaAddDto
//                    {
//                        SesionId = nuevaSesion.SesionId,
//                        PacientesId = addSesionDto.PacienteIds
//                    });
//                }
//                return (true, "Sesión agregada correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al agregar la sesión: {ex.Message}");
//            }
//        }

//        public async Task<(bool, string)> UpdateAsync(SesionDto sesionDto, int sesionId)
//        {
//            if (sesionDto.SesionId != sesionId)
//                return (false, "El ID de la sesión no coincide.");

//            var sesion = await _context.sesion.FindAsync(sesionId);
//            if (sesion == null)
//                return (false, "Sesión no encontrada.");

//            sesion.Fecha = sesionDto.Fecha;
//            sesion.Hora = sesionDto.Hora;
//            sesion.InstructorId = sesionDto.InstructorId;
//            sesion.CaballoId = sesionDto.CaballoId;

//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Sesión actualizada correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al actualizar la sesión: {ex.Message}");
//            }
//        }

//        public async Task<(bool, string)> DeleteAsync(int sesionId)
//        {
//            var sesion = await _context.sesion.FindAsync(sesionId);
//            if (sesion == null)
//                return (false, "Sesión no encontrada.");
//            var asistencias = await _context.asistencia
//                .Where(a => a.SesionId == sesionId)
//                .ToListAsync();
//            try
//            {
//                _context.asistencia.RemoveRange(asistencias);
//                _context.sesion.Remove(sesion);
//                await _context.SaveChangesAsync();
//                return (true, "Sesión eliminada correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al eliminar la sesión: {ex.Message}");
//            }
//        }

//        public async Task<(IEnumerable<SesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek)
//        {
//            try
//            {
//                // La semana es de lunes a domingo
//                var endOfWeek = startOfWeek.AddDays(6);

//                var sesiones = await _context.sesion
//                    .Where(s => s.Fecha >= startOfWeek && s.Fecha <= endOfWeek)
//                    .Select(s => new SesionDto
//                    {
//                        SesionId = s.SesionId,
//                        Fecha = s.Fecha,
//                        Hora = s.Hora,
//                        InstructorId = s.InstructorId,
//                        CaballoId = s.CaballoId
//                    })
//                    .ToListAsync();

//                if (sesiones.Count == 0)
//                    return (sesiones, "No se encontraron sesiones para la semana.");

//                return (sesiones, "Sesiones de la semana obtenidas correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return (Enumerable.Empty<SesionDto>(), $"Error al obtener sesiones: {ex.Message}");
//            }
//        }
//    }
//}