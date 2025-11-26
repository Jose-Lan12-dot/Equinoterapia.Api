using Equinoterapia.Application.DTOs.Sesiones;
using Equinoterapia.Application.DTOs.Shared;
using Equinoterapia.Application.DTOs.Asistencias;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class SesionService : ISesionService
    {
        private readonly ISesionRepository _sesionRepository;
        private readonly ISesionQueryRepository _sesionQueryRepository;
        private readonly IAsistenciaService _asistenciaService;

        private readonly IPacienteQueryRepository _pacienteQueryRepository;
        private readonly IInstructorQueryRepository _instructorQueryRepository;
        private readonly ICaballoQueryRepository _caballoQueryRepository;

        public SesionService(
            ISesionRepository sesionRepository,
            ISesionQueryRepository sesionQueryRepository,
            IAsistenciaService asistenciaService, 

            IPacienteQueryRepository pacienteQueryRepository,
            IInstructorQueryRepository instructorQueryRepository,
            ICaballoQueryRepository caballoQueryRepository)
        {
            _sesionRepository = sesionRepository;
            _sesionQueryRepository = sesionQueryRepository;
            _asistenciaService = asistenciaService;

            _pacienteQueryRepository = pacienteQueryRepository;
            _instructorQueryRepository = instructorQueryRepository;
            _caballoQueryRepository = caballoQueryRepository;
        }

        public async Task<(IEnumerable<GetAllSesionDto>, string)> GetAllAsync()
        {
            try
            {
                var sesiones = await _sesionQueryRepository.GetAllDtosAsync();
                if (!sesiones.Any())
                    return (sesiones, "No se encontraron sesiones.");
                return (sesiones, "Sesiones obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<GetAllSesionDto>(), $"Error al obtener sesiones: {ex.Message}");
            }
        }

        public async Task<(IEnumerable<GetAllSesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek)
        {
            try
            {
                var sesiones = await _sesionQueryRepository.GetByWeekDtosAsync(startOfWeek);
                if (!sesiones.Any())
                    return (sesiones, "No se encontraron sesiones para la semana.");
                return (sesiones, "Sesiones de la semana obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<GetAllSesionDto>(), $"Error al obtener sesiones: {ex.Message}");
            }
        }

        public async Task<(GetOneSesionDto?, string)> GetByIdAsync(int sesionId)
        {
            try
            {
                var sesion = await _sesionRepository.GetByIdAsync(sesionId);
                if (sesion == null)
                    return (null, "Sesión no encontrada.");

                var dto = new GetOneSesionDto
                {
                    SesionId = sesion.SesionId,
                    Fecha = sesion.Fecha,
                    Hora = sesion.Hora,
                    InstructorId = sesion.InstructorId,
                    CaballoId = sesion.CaballoId,
                    nombreInstructor = sesion.Instructor.Nombre,
                    nombreCaballo = sesion.Caballo.Nombre
                };
                return (dto, "Sesión encontrada.");
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener la sesión: {ex.Message}");
            }
        }

        public async Task<(bool, string)> AddAsync(AddSesionDto addSesionDto)
        {
            const int duracionSesionMinutos = 20;
            var nuevaHoraInicio = addSesionDto.Hora;
            var nuevaHoraFin = nuevaHoraInicio.AddMinutes(duracionSesionMinutos);

    
            var HorariosOcupados = await _sesionRepository.GetByFechaAsync(addSesionDto.Fecha);

            foreach (var Horario in HorariosOcupados)
            {
                var actualHoraInicio = Horario.Hora;
                var actualHoraFin = actualHoraInicio.AddMinutes(duracionSesionMinutos);
                if (nuevaHoraInicio < actualHoraFin && actualHoraInicio < nuevaHoraFin)
                {
                    return (false, $"El horario de {nuevaHoraInicio:HH:mm} a {nuevaHoraFin:HH:mm} ya está ocupado por otra sesión.");
                }
            }

            var nuevaSesion = new Sesion
            {
                Fecha = addSesionDto.Fecha,
                Hora = addSesionDto.Hora,
                InstructorId = addSesionDto.InstructorId,
                CaballoId = addSesionDto.CaballoId
            };

            try
            {

                _sesionRepository.Add(nuevaSesion);
                await _sesionRepository.SaveChangesAsync(); 

                if (addSesionDto.PacienteIds != null && addSesionDto.PacienteIds.Any())
                {
                    var (exito, mensaje) = await _asistenciaService.AddAsync(new AddAsistenciaDto
                    {
                        SesionId = nuevaSesion.SesionId,
                        PacientesId = addSesionDto.PacienteIds
                    });

                    if (!exito)
                    {
                        return (true, $"Sesión creada, pero error al agregar asistencias: {mensaje}");
                    }
                }
                return (true, "Sesión agregada correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar la sesión: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdateAsync(UpdateSesionDto sesionDto, int sesionId)
        {
            var sesion = await _sesionRepository.GetByIdAsync(sesionId);
            if (sesion == null)
                return (false, "Sesión no encontrada.");

            if (sesionDto.Fecha.HasValue || sesionDto.Hora.HasValue)
            {
                const int duracionSesionMinutos = 20;
                var nuevaHoraInicio =  sesionDto.Hora ?? sesion.Hora;
                var nuevaHoraFin = nuevaHoraInicio.AddMinutes(duracionSesionMinutos);

                var fechaNueva = sesionDto.Fecha ?? sesion.Fecha;

                var sesionesDelDia = await _sesionRepository.GetByFechaAsync(fechaNueva);

                foreach (var sesionExistente in sesionesDelDia)
                {
                    if (sesionExistente.SesionId == sesionId) continue;

                    var dbHoraInicio = sesionExistente.Hora;
                    var dbHoraFin = dbHoraInicio.AddMinutes(duracionSesionMinutos);

                    if (nuevaHoraInicio < dbHoraFin && dbHoraInicio < nuevaHoraFin)
                    {
                        return (false, $"El nuevo horario choca con la sesión de {dbHoraInicio:HH:mm}.");
                    }
                }

                sesion.Fecha = fechaNueva;
                sesion.Hora = nuevaHoraInicio;
            }

            if (sesionDto.InstructorId.HasValue)
            {
                sesion.InstructorId = sesionDto.InstructorId.Value;
            }

            if (sesionDto.CaballoId.HasValue)
            {
                sesion.CaballoId = sesionDto.CaballoId.Value;
            }

            try
            {
                await _sesionRepository.SaveChangesAsync();
                return (true, "Sesión actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar la sesión: {ex.Message}");
            }
        }

        public async Task<(bool, string)> DeleteAsync(int sesionId)
        {
            var sesion = await _sesionRepository.GetByIdAsync(sesionId);
            if (sesion == null)
                return (false, "Sesión no encontrada.");

            try
            {
                var asistencias = await _sesionRepository.GetAsistenciasBySesionIdAsync(sesionId);
                if (asistencias.Any())
                {
                    _sesionRepository.RemoveAsistencias(asistencias);
                }

                _sesionRepository.Delete(sesion);

                await _sesionRepository.SaveChangesAsync();
                return (true, "Sesión eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar la sesión: {ex.Message}");
            }
        }




        //--- Métodos Adicionales ---



        public async Task<SesionFormDataDto> GetFormDataAsync()
        {
            return new SesionFormDataDto
            {
                Pacientes = await _pacienteQueryRepository.GetActivosAsync(),
                Instructores = await _instructorQueryRepository.GetActivosAsync(),
                Caballos = await _caballoQueryRepository.GetDisponiblesAsync(),
            };
        }


        public async Task<List<HorarioOcupadoDto>> GetHorariosOcupadosAsync(DateOnly fecha)
        {
            const int duracionSesionMinutos = 20;

            var sesionesDelDia = await _sesionRepository.GetByFechaAsync(fecha);

            var horariosOcupados = sesionesDelDia.Select(sesion => new HorarioOcupadoDto
            {
                HoraInicio = sesion.Hora,
                HoraFin = sesion.Hora.AddMinutes(duracionSesionMinutos)
            })
            .OrderBy(h => h.HoraInicio)
            .ToList();

            return horariosOcupados;
        }


    }
}