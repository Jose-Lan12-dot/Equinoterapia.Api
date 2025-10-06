using APIproyectoEquinoterapia.Models;
using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Bson;

namespace APIproyectoEquinoterapia.Services
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
            var formulario = await _context.formulario.FindAsync(pacienteId);

            if (formulario == null)
            {
                var mensaje = "Formulario no encontrado";
                return (null, mensaje);
            }

            var nombrePaciente = await _context.paciente.FindAsync(formulario.PacienteId);
            var preguntas = await GetPreguntas();
            var respuestas = await GetRespuestas(formulario.FormularioId);


            return (new FormularioDTO
            {
                NombrePaciente = nombrePaciente?.Nombre ?? string.Empty,
                Fecha = formulario.Fecha,
                Preguntas = preguntas,
                Respuestas = respuestas
            }, "Formulario Encontrado");

        }


        public async Task<ICollection<FormularioPreguntaDTO>> GetPreguntas() { 

            return await _context.formularioPregunta
                .OrderBy(p => p.PreguntaId)
                .Select(p => new FormularioPreguntaDTO
                {
                    PreguntaIdOrden = p.PreguntaId,
                    Pregunta = p.Pregunta,
                })
                .ToListAsync();
        }

        private async Task<ICollection<FormularioRespuestaDTO>> GetRespuestas(int  formularioId) { 

            return await _context.formularioRespuesta
                .Where(r => r.FormularioId == formularioId)
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

            var numeroPreguntas = await _context.formularioPregunta.CountAsync();
            var numeroRespuestas = formularioDTO.Respuestas.Count;
            if (numeroPreguntas != numeroRespuestas)
            {
                var mensaje = "El número de respuestas no coincide con el número de preguntas";
                return (false, mensaje);
            }

            if (!_context.paciente.Any(p => p.PacienteId == pacienteId))
            {
                var mensaje = "Paciente no encontrado";
                return (false, mensaje);
            }

            if (_context.formulario.Any(f => f.PacienteId == pacienteId))
            {
                var mensaje = "Ya existe un formulario para este paciente";
                return (false, mensaje);
            }

            var nuevoFormulario = new Formulario
            {
                PacienteId = pacienteId,
                Fecha = formularioDTO.Fecha,
            };

            _context.formulario.Add(nuevoFormulario);
            await _context.SaveChangesAsync();
            AgregarRespuesta(formularioDTO.Respuestas, numeroRespuestas, nuevoFormulario.FormularioId);
            await _context.SaveChangesAsync();

            return (true, "Formulario guardado correctamente");
        }


        public void AgregarRespuesta(ICollection<FormularioRespuestaDTO> respuestas, int numeroRespuestas, int formularioId) 
        {
            for (int i = 0; i < numeroRespuestas; i++)
            {
                var nuevaRespuesta = new FormularioRespuestas
                {
                    FormularioId = formularioId,
                    PreguntaId = respuestas.ElementAtOrDefault(i)?.PreguntaIdOrden ?? 0,
                    Respuesta = respuestas.ElementAtOrDefault(i)?.Respuesta ?? 0
                };
                _context.formularioRespuesta.Add(nuevaRespuesta);
            }

        }

        
    }
}
