using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Bson;
using System.Threading.Tasks;
using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.Data.Models.Evaluacion_;
using APIproyectoEquinoterapia.DTOs.Evaluacion_;

namespace APIproyectoEquinoterapia.Data.Services.Paciente_
{
    public class EvaluacionService
    {

        private readonly AppDbContext _context;

        public EvaluacionService(AppDbContext context) 
        {
            _context = context;
        }


        public async Task<(EvaluacionDTO?,string)> GetEvaluacionAsync(int pacienteId)
        {
            var evaluacion = await _context.evaluacion.FirstOrDefaultAsync(e => e.PacienteId == pacienteId);

            if (EsNulo(evaluacion))
            {
                var mensaje = "Evaluacion no encontrada";
                return (null, mensaje);
            }

            var nombrePaciente = await _context.paciente
                .Where(p => p.PacienteId == evaluacion.PacienteId)
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            var paciente = await _context.paciente.FindAsync(evaluacion.PacienteId);
            if (EsNulo(paciente))
            {
                var mensaje = "Paciente no encontrado";
                return (null, mensaje);
            }

            var (preguntas,aux) = await GetPreguntasAsync();
            if (preguntas.Count == 0)
            {
                return (null, aux);
            }

            var respuestas = await GetRespuestas(evaluacion.EvaluacionId);
            if (respuestas.Count == 0)
            {
                return (null, "Respuestas no encontradas");
            }

            return (new EvaluacionDTO
            {
                NombrePaciente = paciente?.Nombre ?? string.Empty,
                Fecha = evaluacion.Fecha ?? DateOnly.MinValue,
                Preguntas = preguntas,
                Respuestas = respuestas
            }, "Evaluacion Encontrada");

        }


        public async Task<(ICollection<PreguntasDTO>,string)> GetPreguntasAsync() { 

            var preguntas = await _context.preguntas
                .OrderBy(p => p.PreguntaId)
                .Select(p => new PreguntasDTO
                {
                    Orden = p.PreguntaId,
                    Pregunta = p.Pregunta,
                })
                .ToListAsync();

            if (preguntas.Count == 0)
            {
                var mensaje = "Preguntas no ncontradas";
                return (preguntas, mensaje);
            }

            return (preguntas,"Preguntas encontradas");
        }

        private async Task<ICollection<RespuestasDTO>> GetRespuestas(int  evaluacionId) { 

            return await _context.respuestas
                .Where(r => r.EvaluacionId == evaluacionId)
                .OrderBy(r => r.PreguntaId)
                .Select(r => new RespuestasDTO
                {
                    PreguntaId = r.PreguntaId,
                    Respuesta = r.Respuesta
                })
                .ToListAsync();
        }


        public async Task<(bool, string)> PostEvaluacionAsync(EvaluacionDTO evaluacionDTO, int pacienteId)
        {

            if (EsNulo(evaluacionDTO) || EsNulo(evaluacionDTO.Respuestas) 
                ||!evaluacionDTO.Respuestas.Any())
            {
                var mensaje = "Evaluacion vacia";
                return (false, mensaje);
            }

            var numeroPreguntas = await _context.preguntas.CountAsync();
            var numeroRespuestas = evaluacionDTO.Respuestas.Count;
            if (numeroPreguntas != numeroRespuestas)
            {
                var mensaje = "El número de respuestas no coincide con el número de preguntas";
                return (false, mensaje);
            }

            if (!await PacienteExiste(pacienteId))
            {
                var mensaje = "Paciente_ no encontrado";
                return (false, mensaje);
            }

            if (await EvaluacionExiste(pacienteId))
            {
                var mensaje = "Ya existe un evaluacion para este paciente";
                return (false, mensaje);
            }

            var nuevaEvaluacion = new Evaluacion
            {
                PacienteId = pacienteId,
                Fecha = evaluacionDTO.Fecha,
            };

            _context.evaluacion.Add(nuevaEvaluacion);
            await _context.SaveChangesAsync();
            AgregarRespuesta(evaluacionDTO.Respuestas, numeroRespuestas, nuevaEvaluacion.EvaluacionId);
            await _context.SaveChangesAsync();

            return (true, "Evaluacion guardada correctamente");
        }


        private void AgregarRespuesta(ICollection<RespuestasDTO> respuestas, int numeroRespuestas, int evaluacionId) 
        {
            for (int i = 0; i < numeroRespuestas; i++)
            {
                var nuevaRespuesta = new Respuestas
                {
                    EvaluacionId = evaluacionId,
                    PreguntaId = respuestas.ElementAtOrDefault(i)?.PreguntaId ?? 0,
                    Respuesta = respuestas.ElementAtOrDefault(i)?.Respuesta ?? 0
                };
                _context.respuestas.Add(nuevaRespuesta);
            }

        }

        public async Task<(bool, string)> PutEvaluacionAsync(EvaluacionDTO evaluacionDTO, int pacienteId)
        {

            if (EsNulo(evaluacionDTO) || EsNulo(evaluacionDTO.Respuestas) 
                || !evaluacionDTO.Respuestas.Any())
            {
                var mensaje = "Evaluacion vacia";
                return (false, mensaje);
            }

            if (!await PacienteExiste(pacienteId))
            {
                var mensaje = "Paciente_ no encontrado";
                return (false, mensaje);
            }

            var evaluacion = await _context.evaluacion.FirstOrDefaultAsync(e => e.PacienteId == pacienteId);
            if (EsNulo(evaluacion))
            {
                var mensaje = "La evaluación no existe";
                return (false, mensaje);
            }
            evaluacion.Fecha = evaluacionDTO.Fecha;
            await _context.SaveChangesAsync();
            await ActualizarRespuesta(evaluacionDTO.Respuestas, evaluacion.EvaluacionId);
            await _context.SaveChangesAsync();

            return (true, "Evaluacion actualizada correctamente");
        }


        private async Task ActualizarRespuesta(ICollection<RespuestasDTO> nuevasRespuestas, int evaluacionId)
        {
            var listaRespuestas = await _context.respuestas
                .Where(r => r.EvaluacionId == evaluacionId)
                .ToListAsync();

            foreach (var nuevaRespuesta in nuevasRespuestas)
            {
                var respuestaActual = listaRespuestas
                    .FirstOrDefault(r => r.PreguntaId == nuevaRespuesta.PreguntaId);

                if (!EsNulo(respuestaActual))
                {
                    respuestaActual.Respuesta = nuevaRespuesta.Respuesta;
                    _context.respuestas.Update(respuestaActual);
                }
            }
        }

        public async Task<(bool, string)> DeleteEvaluacionAsync(int pacienteId)
        {

            var evaluacion = await _context.evaluacion.FirstOrDefaultAsync(e => e.PacienteId == pacienteId);
            if (EsNulo(evaluacion))
            {
                var mensaje = "La evaluacion no exite";
                return (false, mensaje);
            }

            var respuestas = await _context.respuestas
                .Where(r => r.EvaluacionId == evaluacion.EvaluacionId)
                .ToListAsync();

            _context.respuestas.RemoveRange(respuestas);

            _context.evaluacion.Remove(evaluacion);

            await _context.SaveChangesAsync();

            return (true, "Evaluacion borrado correctamente");
        }


        public bool EsNulo(object? objeto)
        {
            if (objeto == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PacienteExiste(int pacienteId)
        {
            return await _context.paciente.AnyAsync(p => p.PacienteId == pacienteId);
        }

        public async Task<bool> EvaluacionExiste(int pacienteId)
        {
            return await _context.evaluacion.AnyAsync(e => e.PacienteId == pacienteId);
        }

    }
}
