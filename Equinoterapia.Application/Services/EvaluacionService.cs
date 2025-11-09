using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IEvaluacionQueryRepository _evaluacionQueryRepository;
        private readonly IRespuestasService _respuestasService;

        public EvaluacionService(IEvaluacionRepository evaluacionRepository,
            IEvaluacionQueryRepository evaluacionQueryRepository,
            IRespuestasService respuestasService)
        {
            _evaluacionRepository = evaluacionRepository;
            _evaluacionQueryRepository = evaluacionQueryRepository;
            _respuestasService = respuestasService;
        }

        public async Task<(EvaluacionDto?, string?, string)> GetAsync(int pacienteId)
        {
            if (!await _evaluacionRepository.PacienteExistsAsync(pacienteId))
            {
                return (null, "", "Paciente no encontrado");
            }

            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (null, "", "Evaluacion no encontrada");
            }

            var nombrePaciente = await _evaluacionRepository.GetPacienteNombreAsync(pacienteId);

            var (preguntas, msgPreg) = await GetPreguntasAsync();
            if (!preguntas.Any())
            {
                return (null, "", msgPreg);
            }

            // Orquestación del servicio de respuestas
            var (respuestas, msgResp) = await _respuestasService.Get(evaluacion.EvaluacionId);
            if (!respuestas.Any())
            {
                return (null, "", msgResp);
            }

            return (new EvaluacionDto
            {
                Fecha = evaluacion.Fecha,
                Preguntas = preguntas,
                Respuestas = respuestas
            }, nombrePaciente, "Evaluacion Encontrada");
        }

        public async Task<(bool, string)> AddAsync(EvaluacionDto evaluacionDto, int pacienteId)
        {
            if (!await _evaluacionRepository.PacienteExistsAsync(pacienteId))
            {
                return (false, "Paciente no encontrado");
            }

            if (ValidateDataEvaluacion(evaluacionDto))
            {
                return (false, "Datos de evaluación inválidos");
            }

            var numeroPreguntas = await _evaluacionRepository.GetPreguntasCountAsync();
            var numeroRespuestas = evaluacionDto.Respuestas.Count();
            if (numeroPreguntas != numeroRespuestas)
            {
                return (false, "El número de respuestas no coincide con el número de preguntas");
            }

            if (await _evaluacionRepository.EvaluacionExistsAsync(pacienteId))
            {
                return (false, "Ya existe un evaluacion para este paciente");
            }

            // Lógica de Mapeo
            var nuevaEvaluacion = new Evaluacion
            {
                PacienteId = pacienteId,
                Fecha = evaluacionDto.Fecha
            };

            try
            {
                _evaluacionRepository.Add(nuevaEvaluacion);
                await _evaluacionRepository.SaveChangesAsync(); // Guardamos para obtener el ID

                // Orquestación del servicio de respuestas
                var (exito, mensaje) = await _respuestasService.Add(evaluacionDto.Respuestas, nuevaEvaluacion.EvaluacionId);
                if (!exito)
                {
                    // (Considerar "Rollback": Borrar la evaluación si las respuestas fallan)
                    return (false, mensaje);
                }

                return (true, "Evaluacion guardada correctamente");
            }
            catch (Exception ex)
            {
                return (false, "Error al guardar la evaluacion: " + ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(EvaluacionDto evaluacionDto, int pacienteId)
        {
            if (!await _evaluacionRepository.PacienteExistsAsync(pacienteId))
            {
                return (false, "Paciente_ no encontrado");
            }

            if (ValidateDataEvaluacion(evaluacionDto))
            {
                return (false, "Datos de evaluación inválidos");
            }

            var numeroPreguntas = await _evaluacionRepository.GetPreguntasCountAsync();
            var numeroRespuestas = evaluacionDto.Respuestas.Count();
            if (numeroPreguntas != numeroRespuestas)
            {
                return (false, "El número de respuestas no coincide con el número de preguntas");
            }

            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (false, "La evaluación no existe");
            }

            // Lógica de Negocio
            evaluacion.Fecha = evaluacionDto.Fecha;
            await _evaluacionRepository.SaveChangesAsync(); // Guardamos el cambio de fecha

            try
            {
                // Orquestación
                var (exito, mensaje) = await _respuestasService.Update(evaluacionDto.Respuestas, evaluacion.EvaluacionId);
                if (!exito)
                {
                    return (false, mensaje);
                }
                return (true, "Evaluacion actualizada correctamente");
            }
            catch (Exception ex)
            {
                return (false, "Error al actualizar la evaluacion: " + ex.Message);
            }
        }

        public async Task<(bool, string)> DeleteAsync(int pacienteId)
        {
            if (!await _evaluacionRepository.PacienteExistsAsync(pacienteId))
            {
                return (false, "Paciente no encontrado");
            }

            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (false, "La evaluacion no exite");
            }

            try
            {
                // Orquestación: Primero borra las dependencias
                var (exito, mensaje) = await _respuestasService.Delete(evaluacion.EvaluacionId);
                if (!exito)
                {
                    return (false, mensaje);
                }

                // Luego borra la evaluación principal
                _evaluacionRepository.Delete(evaluacion);
                await _evaluacionRepository.SaveChangesAsync();

                return (true, "Evaluacion eliminada correctamente");
            }
            catch (Exception ex)
            {
                return (false, "Error al eliminar la evaluacion: " + ex.Message);
            }
        }

        public async Task<(IEnumerable<PreguntasDto>, string)> GetPreguntasAsync()
        {
            var preguntas = await _evaluacionQueryRepository.GetPreguntasDtoAsync();
            if (!preguntas.Any())
            {
                return (preguntas, "Preguntas no ncontradas");
            }
            return (preguntas, "Preguntas encontradas");
        }

        // --- MÉTODOS PRIVADOS (Helper) ---

        private bool IsNull(object? objeto) => objeto == null;

        public bool ValidateDataEvaluacion(EvaluacionDto evaluacionDto)
        {
            if (IsNull(evaluacionDto) || IsNull(evaluacionDto.Respuestas))
            {
                return true;
            }
            return false;
        }
    }
}