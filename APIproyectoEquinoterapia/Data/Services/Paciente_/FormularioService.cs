using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Bson;
using System.Threading.Tasks;
using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.Data.Models.Evaluacion_;

namespace APIproyectoEquinoterapia.Data.Services.Paciente_
{
    public class FormularioService
    {

        private readonly AppDbContext _context;

        public FormularioService(AppDbContext context) 
        {
            _context = context;
        }


        public async Task<(FormularioDTO?,string)> GetFormularioAsync(int pacienteId)
        {
            var formulario = await _context.evaluacion.FirstOrDefaultAsync(f => f.PacienteId == pacienteId);

            if (formulario == null)
            {
                var mensaje = "Formulario no encontrado";
                return (null, mensaje);
            }

            var nombrePaciente = await _context.paciente.FindAsync(formulario.PacienteId);
            var (preguntas,aux) = await GetPreguntasAsync();
            var respuestas = await GetRespuestas(formulario.EvaluacionId);


            return (new FormularioDTO
            {
                NombrePaciente = nombrePaciente?.Nombre ?? string.Empty,
                Fecha = formulario.Fecha,
                Preguntas = preguntas,
                Respuestas = respuestas
            }, "Formulario Encontrado");

        }


        public async Task<(ICollection<FormularioPreguntaDTO>,string)> GetPreguntasAsync() { 

            var preguntas = await _context.preguntas
                .OrderBy(p => p.PreguntaId)
                .Select(p => new FormularioPreguntaDTO
                {
                    PreguntaIdOrden = p.PreguntaId,
                    Pregunta = p.Pregunta,
                })
                .ToListAsync();

            if (!preguntas.Any()) 
            {
                var mensaje = "Preguntas no ncontradas";
                return (preguntas, mensaje);
            }
                

            return (preguntas,"Preguntas encontradas");
        }

        private async Task<ICollection<FormularioRespuestaDTO>> GetRespuestas(int  formularioId) { 

            return await _context.respuestas
                .Where(r => r.EvaluacionId == formularioId)
                .OrderBy(r => r.PreguntaId)
                .Select(r => new FormularioRespuestaDTO
                {
                    PreguntaIdOrden = r.PreguntaId,
                    Respuesta = r.Respuesta
                })
                .ToListAsync();
        }


        public async Task<(bool, string)> PostFormularioAsync(FormularioDTO formularioDTO, int pacienteId)
        {

            if (formularioDTO == null || formularioDTO.Respuestas == null)
            {
                var mensaje = "Formulario vacio";
                return (false, mensaje);
            }

            var numeroPreguntas = await _context.preguntas.CountAsync();
            var numeroRespuestas = formularioDTO.Respuestas.Count;
            if (numeroPreguntas != numeroRespuestas)
            {
                var mensaje = "El número de respuestas no coincide con el número de preguntas";
                return (false, mensaje);
            }

            if (!_context.paciente.Any(p => p.PacienteId == pacienteId))
            {
                var mensaje = "Paciente_ no encontrado";
                return (false, mensaje);
            }

            if (_context.evaluacion.Any(f => f.PacienteId == pacienteId))
            {
                var mensaje = "Ya existe un formulario para este paciente";
                return (false, mensaje);
            }

            var nuevoFormulario = new Evaluacion
            {
                PacienteId = pacienteId,
                Fecha = formularioDTO.Fecha,
            };

            _context.evaluacion.Add(nuevoFormulario);
            await _context.SaveChangesAsync();
            AgregarRespuesta(formularioDTO.Respuestas, numeroRespuestas, nuevoFormulario.EvaluacionId);
            await _context.SaveChangesAsync();

            return (true, "Formulario guardado correctamente");
        }


        private void AgregarRespuesta(ICollection<FormularioRespuestaDTO> respuestas, int numeroRespuestas, int formularioId) 
        {
            for (int i = 0; i < numeroRespuestas; i++)
            {
                var nuevaRespuesta = new Respuestas
                {
                    EvaluacionId = formularioId,
                    PreguntaId = respuestas.ElementAtOrDefault(i)?.PreguntaIdOrden ?? 0,
                    Respuesta = respuestas.ElementAtOrDefault(i)?.Respuesta ?? 0
                };
                _context.respuestas.Add(nuevaRespuesta);
            }

        }

        public async Task<(bool, string)> PutFormularioAsync(FormularioDTO formularioDTO, int pacienteId)
        {

            if (formularioDTO == null || formularioDTO.Respuestas == null)
            {
                var mensaje = "Formulario vacio";
                return (false, mensaje);
            }


            if (!_context.paciente.Any(p => p.PacienteId == pacienteId))
            {
                var mensaje = "Paciente_ no encontrado";
                return (false, mensaje);
            }

            var formulario = await _context.evaluacion.FirstOrDefaultAsync(f => f.PacienteId == pacienteId);
            if (formulario == null)
            {
                var mensaje = "El formulario no exite";
                return (false, mensaje);
            }


            await _context.SaveChangesAsync();
            await ActualizarRespuesta(formularioDTO.Respuestas, formulario.EvaluacionId);
            await _context.SaveChangesAsync();

            return (true, "Formulario actualizado correctamente");
        }


        private async Task ActualizarRespuesta(ICollection<FormularioRespuestaDTO> NuevasRespuestas, int formularioId)
        {
            var listaRespuestas = await _context.respuestas
                .Where(r => r.EvaluacionId == formularioId)
                .ToListAsync();

            foreach (var nuevaRespuesta in NuevasRespuestas)
            {
                var respuestaActual = listaRespuestas
                    .FirstOrDefault(r => r.PreguntaId == nuevaRespuesta.PreguntaIdOrden);

                if (respuestaActual != null)
                {
                    respuestaActual.Respuesta = nuevaRespuesta.Respuesta;
                    _context.respuestas.Update(respuestaActual);
                }
            }
        }

        public async Task<(bool, string)> DeleteFormularioAsync(int pacienteId)
        {

            var formulario = await _context.evaluacion.FirstOrDefaultAsync(f => f.PacienteId == pacienteId);
            if (formulario == null)
            {
                var mensaje = "El formulario no exite";
                return (false, mensaje);
            }

            var respuestas = await _context.respuestas
                .Where(r => r.EvaluacionId == formulario.EvaluacionId)
                .ToListAsync();

            _context.respuestas.RemoveRange(respuestas);

            _context.evaluacion.Remove(formulario);

            await _context.SaveChangesAsync();

            return (true, "Formulario borrado correctamente");
        }


    }
}
