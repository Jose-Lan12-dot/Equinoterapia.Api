//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.Data.Models.Evaluacion_;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualBasic;
//using System.Collections;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class RespuestasService
//    {
//        private readonly AppDbContext _context;

//        public RespuestasService(AppDbContext context)
//        {
//            _context = context;
//        }

////////////////////////
//        public async Task<(IEnumerable<RespuestasDto>,string)> Get(int evaluacionId)
//        {
//            try
//            {
//                var respuestas = await GetRespuestas(evaluacionId);
//                if (respuestas.Count == 0)
//                {
//                    var mensaje = "Respuestas no encontradas";
//                    return (Enumerable.Empty<RespuestasDto>(), mensaje);
//                }
//                return (MapToDto(respuestas), "Respuestas encontradas");
//            }
//            catch (Exception ex)
//            {
//                return (Enumerable.Empty<RespuestasDto>(), $"Error al obtener las respuestas: {ex.Message}");
//            }
//        }

////////////////////////
//        public async Task<(bool,string)> AddInforme(IEnumerable<RespuestasDto> respuestas,int evaluacionId)
//        {
//            foreach (var respuesta in respuestas) 
//            { 
//                var nuevaRespuesta = new Respuestas
//                {
//                    EvaluacionId = evaluacionId,
//                    PreguntaId = respuesta.PreguntaId,
//                    Respuesta = respuesta.Respuesta
//                };
//                _context.respuestas.AddInforme(nuevaRespuesta); 
//            }

//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Respuestas agregadas correctamente");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al agregar las respuestas: {ex.Message}");
//            }
//        }

////////////////////////
//        public async Task<(bool,string)> Update(IEnumerable<RespuestasDto> respuestasNuevas, int evaluacionId)
//        {
//            var respuestasActuales = await GetRespuestas(evaluacionId);

//            foreach (var respuestaActual in respuestasActuales)
//            {
//                var respuestaNueva = respuestasActuales
//                    .FirstOrDefault(r => r.PreguntaId == respuestaActual.PreguntaId);

//                respuestaActual.Respuesta = respuestaNueva!.Respuesta;
//                _context.respuestas.Update(respuestaActual);
//            }
//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Respuestas actualizadas correctamente");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al actualizar las respuestas: {ex.Message}");
//            }
//        }

////////////////////////
//        public async Task<(bool,string)> Delete(int evaluacionId)
//        {
//            var respuestas = await GetRespuestas(evaluacionId);
//            _context.respuestas.RemoveRange(respuestas);
//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Respuestas eliminadas correctamente");
//            }
//            catch (Exception ex)
//            {
//                return (false, $"Error al eliminar las respuestas: {ex.Message}");
//            }
//        }








////////////////////////
//        private async Task<List<Respuestas>> GetRespuestas(int evaluacionId)
//        {
//            var respuestas = await _context.respuestas
//                .Where(r => r.EvaluacionId == evaluacionId)
//                .OrderBy(r => r.PreguntaId)
//                .ToListAsync();
//            return respuestas;
//        }

////////////////////////
//        private List<RespuestasDto> MapToDto(List<Respuestas> respuestas)
//        {
//            return respuestas.Select(r => new RespuestasDto
//            {
//                PreguntaId = r.PreguntaId,
//                Respuesta = r.Respuesta
//            }).ToList();
//        }
//    }
//}
