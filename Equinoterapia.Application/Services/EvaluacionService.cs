using Equinoterapia.Application.DTOs.Evaluaciones;
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


        //public async Task<(GetAllEvaluacionesAsyncDto, string)> GetAllAsync(int pacienteId)
        //{
        //    var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
        //    if (evaluacion == null)
        //    {
        //        return (null, "Evaluacion no encontrada");
        //    }

        //    var nombrePaciente = await _evaluacionRepository.GetPacienteNombreAsync(pacienteId);

        //    var (preguntas, msgPreg) = await GetPreguntasAsync();
        //    if (!preguntas.Any())
        //    {
        //        return (null, msgPreg);
        //    }

        //    var (respuestas, msgResp) = await _respuestasService.Get(evaluacion.EvaluacionId);
        //    if (!respuestas.Any())
        //    {
        //        return (null, msgResp);
        //    }

        //    return (new GetEvaluacionDto
        //    {
        //        Fecha = evaluacion.Fecha,
        //        Preguntas = preguntas,
        //        Respuestas = respuestas,
        //        nombrePaciente = nombrePaciente ?? string.Empty
        //    }, "Evaluacion Encontrada");
        //}

        public async Task<(GetEvaluacionDto?, string)> GetAsync(int pacienteId)
        {   
            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (null, "Evaluacion no encontrada");
            }

            var nombrePaciente = await _evaluacionRepository.GetPacienteNombreAsync(pacienteId);

            var (preguntas, msgPreg) = await GetPreguntasAsync();
            if (!preguntas.Any())
            {
                return (null, msgPreg);
            }

            var (respuestas, msgResp) = await _respuestasService.Get(evaluacion.EvaluacionId);
            if (!respuestas.Any())
            {
                return (null, msgResp);
            }

            return (new GetEvaluacionDto
            {
                Fecha = evaluacion.Fecha,
                Preguntas = preguntas,
                Respuestas = respuestas,
                nombrePaciente = nombrePaciente ?? string.Empty
            }, "Evaluacion Encontrada");
        }

        public async Task<(bool, string)> AddAsync(AddEvaluacionDto evaluacionDto, int pacienteId)
        {
            var fecha = evaluacionDto.Fecha;
            var respuestas = evaluacionDto.Respuestas;


            if (!await _evaluacionRepository.PacienteExistsAsync(pacienteId))
            {
                return (false, "Paciente no encontrado");
            }


            var numeroPreguntas = await _evaluacionRepository.GetPreguntasCountAsync();
            var numeroRespuestas = respuestas.Count();
            if (numeroPreguntas != numeroRespuestas)
            {
                return (false, "El número de respuestas no coincide con el número de preguntas");
            }

            if (await _evaluacionRepository.EvaluacionExistsAsync(pacienteId))
            {
                return (false, "Ya existe un evaluacion para este paciente");
            }


            var nuevaEvaluacion = new Evaluacion
            {
                PacienteId = pacienteId,
                Fecha = fecha
            };

            try
            {
                _evaluacionRepository.Add(nuevaEvaluacion);
                await _evaluacionRepository.SaveChangesAsync();

                
                var (exito, mensaje) = await _respuestasService.Add(respuestas, nuevaEvaluacion.EvaluacionId);
                if (!exito)
                {
                    await DeleteAsync(pacienteId);
                    return (false, mensaje);
                }

                return (true, "Evaluacion guardada correctamente");
            }
            catch (Exception ex)
            {
                return (false, "Error al guardar la evaluacion: " + ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(UpdateEvaluacionDto evaluacionDto, int pacienteId)
        {
            var fecha = evaluacionDto.Fecha;
            var respuestas = evaluacionDto.Respuestas;

            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (false, "La evaluación no existe");
            }


            if (fecha.HasValue) 
            {
                evaluacion.Fecha = fecha.Value;
            }

            try
            {
                await _evaluacionRepository.SaveChangesAsync();
                if (respuestas != null && respuestas.Count > 0)
                {
                    var (exito, mensaje) = await _respuestasService.Update(respuestas, evaluacion.EvaluacionId);
                    if (!exito)
                    {
                        return (true, "Se actualizo la fecha pero hubo problemas con las respuestas");
                    }
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

            var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
            if (evaluacion == null)
            {
                return (false, "La evaluacion no exite");
            }

            try
            {
                var (exito, mensaje) = await _respuestasService.Delete(evaluacion.EvaluacionId);
                if (!exito)
                {
                    return (false, mensaje);
                }

                _evaluacionRepository.Delete(evaluacion);
                await _evaluacionRepository.SaveChangesAsync();

                return (true, "Evaluacion eliminada correctamente");
            }
            catch (Exception ex)
            {
                return (false, "Error al eliminar la evaluacion: " + ex.Message);
            }
        }

        public async Task<(List<PreguntasDto>, string)> GetPreguntasAsync()
        {
            var preguntas = await _evaluacionQueryRepository.GetPreguntasDtoAsync();
            if (!preguntas.Any())
            {
                return (preguntas, "Preguntas no ncontradas");
            }
            return (preguntas, "Preguntas encontradas");
        }


    }
}