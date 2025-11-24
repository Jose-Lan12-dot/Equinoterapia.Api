using Equinoterapia.Application.DTOs.Asistencias;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services 
{
    public class AsistenciaService : IAsistenciaService 
    {

        private readonly IAsistenciaRepository _repository;
        private readonly IAsistenciaQueryRepository _queryRepository;
        private readonly IPacienteRepository _repositoryPaciente;

        public AsistenciaService(IAsistenciaRepository repository, 
            IAsistenciaQueryRepository queryRepository,
            IPacienteRepository repositoryPaciente)
        {
            _repository = repository;
            _queryRepository = queryRepository;
            _repositoryPaciente = repositoryPaciente;
        }

        public async Task<(IEnumerable<GetAsistenciaDto>, string)> GetAsync(int sesionId)
        {
            if (!await _repository.SesionExistsAsync(sesionId))
            {
                return (Enumerable.Empty<GetAsistenciaDto>(), "La sesion especificada no existe");
            }

            try
            {
                
                var asistencias = await _queryRepository.GetAsistenciasBySesionIdAsync(sesionId);

                if (!asistencias.Any())
                {
                    return (asistencias, "No se encontraron pacientes para la sesion especificada");
                }
                return (asistencias, "Participantes devueltos correctamente");
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<GetAsistenciaDto>(), $"Error al obtener los pacientes participantes: {ex.Message}");
            }
        }

        public async Task<(bool, string)> AddAsync(AddAsistenciaDto asistenciaAdd)
        {
            var sesionId = asistenciaAdd.SesionId;
            var pacientesIds = asistenciaAdd.PacientesId;

            if (!await _repository.SesionExistsAsync(sesionId))
            {
                return (false, "Sesion no existe");
            }

            try
            {
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

        public async Task<(string, bool)> UpdateAsync(UpdateAsistenciaDto asistenciaUpdate)
        {
            try
            {
                var updatesDict = asistenciaUpdate.Asistencias.ToDictionary(a => a.AsistenciasId);
                var asistenciaIds = updatesDict.Keys.ToList();

                var asistenciasEnDb = await _repository.GetAsistenciasByIdsAsync(asistenciaIds);

                foreach (var asistencia in asistenciasEnDb)
                {
                    if (updatesDict.TryGetValue(asistencia.AsistenciaId, out var updateDto))
                    {
                        asistencia.Asistio = updateDto.Asistio;
                    }
                    var faltasPaciente = await _repositoryPaciente.GetFaltasCountAsync(asistencia.PacienteId);
                    if (faltasPaciente >= 3)
                    {
                        var estadoPaciente = await _repositoryPaciente.PacienteEstado(asistencia.PacienteId);
                        estadoPaciente = EstadoPaciente.Baja;
                        await _repositoryPaciente.SaveChangesAsync();
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