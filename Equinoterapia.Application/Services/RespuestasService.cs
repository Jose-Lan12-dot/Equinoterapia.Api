using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces; 
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces; 
namespace Equinoterapia.Application.Services 
{
    public class RespuestasService : IRespuestasService
    {
        private readonly IRespuestaRepository _respuestaRepository;

        public RespuestasService(IRespuestaRepository respuestaRepository)
        {
            _respuestaRepository = respuestaRepository;
        }

        public async Task<(IEnumerable<RespuestasDto>, string)> Get(int evaluacionId)
        {
            try
            {
                var respuestas = await _respuestaRepository.GetRespuestasByEvaluacionIdAsync(evaluacionId);
                if (!respuestas.Any())
                {
                    return (Enumerable.Empty<RespuestasDto>(), "Respuestas no encontradas");
                }
                return (MapToDto(respuestas), "Respuestas encontradas");
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<RespuestasDto>(), $"Error al obtener las respuestas: {ex.Message}");
            }
        }

        public async Task<(bool, string)> Add(IEnumerable<RespuestasDto> respuestas, int evaluacionId)
        {
            
            var nuevasRespuestas = respuestas.Select(respuesta => new Respuestas
            {
                EvaluacionId = evaluacionId,
                PreguntaId = respuesta.PreguntaId,
                Respuesta = respuesta.Respuesta
            });

            try
            {
                _respuestaRepository.AddRange(nuevasRespuestas);
                await _respuestaRepository.SaveChangesAsync();
                return (true, "Respuestas agregadas correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar las respuestas: {ex.Message}");
            }
        }


        //NOTA:Solo se envia la respuesta que se quiera actaulizar
        //evaluacionId = 1
        //preguntaId = 1
        //respueta = 9
        public async Task<(bool, string)> Update(IEnumerable<RespuestasDto> respuestasNuevas, int evaluacionId)
        {
            try
            {
                var respuestasActuales = await _respuestaRepository.GetRespuestasByEvaluacionIdAsync(evaluacionId);

                var respuestasNuevasDict = respuestasNuevas.ToDictionary(r => r.PreguntaId);

                foreach (var respuestaActual in respuestasActuales)
                {
                    if (respuestasNuevasDict.TryGetValue(respuestaActual.PreguntaId, out var respuestaNueva))
                    {
                        respuestaActual.Respuesta = respuestaNueva.Respuesta;
                    }
                }

                await _respuestaRepository.SaveChangesAsync();
                return (true, "Respuestas actualizadas correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar las respuestas: {ex.Message}");
            }
        }

        public async Task<(bool, string)> Delete(int evaluacionId)
        {
            try
            {
                var respuestas = await _respuestaRepository.GetRespuestasByEvaluacionIdAsync(evaluacionId);
                if (!respuestas.Any())
                {
                    return (true, "No había respuestas que eliminar");
                }

                _respuestaRepository.RemoveRange(respuestas);
                await _respuestaRepository.SaveChangesAsync();
                return (true, "Respuestas eliminadas correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar las respuestas: {ex.Message}");
            }
        }



// --- MÉTODOS PRIVADOS (Helper) ---
        private List<RespuestasDto> MapToDto(List<Respuestas> respuestas)
        {
            return respuestas.Select(r => new RespuestasDto
            {
                PreguntaId = r.PreguntaId,
                Respuesta = r.Respuesta
            }).ToList();
        }
    }
}