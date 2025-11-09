using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services 
{
    public class AsistenciaService : IAsistenciaService 
    {
        // --- ¡CAMBIO! ---
        private readonly IAsistenciaRepository _repository;
        private readonly IAsistenciaQueryRepository _queryRepository;

        public AsistenciaService(IAsistenciaRepository repository, IAsistenciaQueryRepository queryRepository)
        {
            _repository = repository;
            _queryRepository = queryRepository;
        }

        public async Task<(IEnumerable<AsistenciaDto>, string)> GetAsync(int sesionId)
        {
            if (!await _repository.SesionExistsAsync(sesionId))
            {
                return (Enumerable.Empty<AsistenciaDto>(), "La sesion especificada no existe");
            }

            try
            {
                // Usamos la consulta optimizada (N+1 resuelto)
                var asistencias = await _queryRepository.GetAsistenciasBySesionIdAsync(sesionId);

                if (!asistencias.Any())
                {
                    return (asistencias, "No se encontraron pacientes para la sesion especificada");
                }
                return (asistencias, "Participantes devueltos correctamente");
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<AsistenciaDto>(), $"Error al obtener los pacientes participantes: {ex.Message}");
            }
        }

        public async Task<(bool, string)> AddAsync(AsistenciaAddDto asistenciaAdd)
        {
            var sesionId = asistenciaAdd.SesionId;
            var pacientesIds = asistenciaAdd.PacientesId;

            // Lógica de validación
            // ¡Corregí tu bug! El 'if' estaba al revés.
            if (!await _repository.SesionExistsAsync(sesionId))
            {
                return (false, "Sesion no existe");
            }

            try
            {
                // Lógica de negocio: solo añade pacientes que sí existen
                var pacientesExistentes = await _repository.GetExistingPacienteIdsAsync(pacientesIds);

                var nuevasAsistencias = pacientesExistentes.Select(pacienteId => new Asistencia
                {
                    SesionId = sesionId,
                    PacienteId = pacienteId,
                    Asistio = false
                });

                _repository.AddRange(nuevasAsistencias);
                await _repository.SaveChangesAsync();

                return (true, "Pacientes agregados correctamnete");
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar los pacientes: {ex.Message}");
            }
        }

        public async Task<(string, bool)> UpdateAsync(AsistenciaUpdateDto asistenciaUpdate)
        {
            try
            {
                // Creamos un diccionario para búsqueda rápida
                var updatesDict = asistenciaUpdate.Asistencias.ToDictionary(a => a.AsistenciasId);
                var asistenciaIds = updatesDict.Keys.ToList();

                // Obtenemos las entidades de la BD
                var asistenciasEnDb = await _repository.GetAsistenciasByIdsAsync(asistenciaIds);

                // Aplicamos los cambios
                foreach (var asistencia in asistenciasEnDb)
                {
                    if (updatesDict.TryGetValue(asistencia.AsistenciaId, out var updateDto))
                    {
                        asistencia.Asistio = updateDto.Asistio;
                    }
                }

                await _repository.SaveChangesAsync();
                return ("Asistencias actualizadas correctamente", true);
            }
            catch (Exception ex)
            {
                return ($"Error al actualizar las asistencias: {ex.Message}", false);
            }
        }

        // Corregido el error tipográfico "DelateAsync"
        public async Task<(string, bool)> DeleteAsync(List<int> asistenciaId)
        {
            if (asistenciaId == null || !asistenciaId.Any())
            {
                return ("No se proporcionaron IDs de asistencias para eliminar", false);
            }

            try
            {
                var asistencias = await _repository.GetAsistenciasByIdsAsync(asistenciaId);

                if (asistencias.Any())
                {
                    _repository.RemoveRange(asistencias);
                    await _repository.SaveChangesAsync();
                }

                return ("Asistencias eliminadas correctamente", true);
            }
            catch (Exception ex)
            {
                return ($"Error al eliminar las asistencias: {ex.Message}", false);
            }
        }
    }
}