using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/pacientes/{pacienteId}/evaluacion")]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService _evaluacionService;

        public EvaluacionController(IEvaluacionService evaluacionService)
        {
            _evaluacionService = evaluacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvaluacion(int pacienteId)
        {
            var (evaluacion, nombrePaciente, mensaje) = await _evaluacionService.GetAsync(pacienteId);

            if (evaluacion == null)
            {
                return NotFound(new { Mensaje = mensaje }); // 404
            }

            // Devolvemos un objeto anónimo con todo
            return Ok(new
            {
                NombrePaciente = nombrePaciente,
                evaluacion.Fecha,
                evaluacion.Preguntas,
                evaluacion.Respuestas
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvaluacion(int pacienteId, [FromBody] EvaluacionDto evaluacionDto)
        {
            // [ApiController] ya valida el ModelState

            var (exito, mensaje) = await _evaluacionService.AddAsync(evaluacionDto, pacienteId);

            if (!exito)
            {
                // 400 Bad Request (lógica fallida, paciente no existe, etc.)
                return BadRequest(new { Mensaje = mensaje });
            }

            // Idealmente, aquí se devolvería un 201 CreatedAtAction
            return Ok(new { Mensaje = mensaje }); // 200
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvaluacion(int pacienteId, [FromBody] EvaluacionDto evaluacionDto)
        {
            var (exito, mensaje) = await _evaluacionService.UpdateAsync(evaluacionDto, pacienteId);

            if (!exito)
            {
                // 404 si no encontró al paciente o la evaluación
                return NotFound(new { Mensaje = mensaje });
            }

            return NoContent(); // 204 (Éxito en actualización)
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvaluacion(int pacienteId)
        {
            var (exito, mensaje) = await _evaluacionService.DeleteAsync(pacienteId);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); // 404
            }

            return NoContent(); // 204
        }


        [HttpGet]
        [Route("/api/preguntas")] // Ruta raíz
        public async Task<IActionResult> GetPreguntas()
        {
            var (preguntas, mensaje) = await _evaluacionService.GetPreguntasAsync();

            if (!preguntas.Any())
            {
                return NotFound(new { Mensaje = mensaje }); // 404
            }

            return Ok(preguntas);
        }
    }
}