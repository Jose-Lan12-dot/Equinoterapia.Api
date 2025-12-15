using Equinoterapia.Application.DTOs.Evaluaciones;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [Authorize]
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
            var (evaluacion, mensaje) = await _evaluacionService.GetAsync(pacienteId);

            if (evaluacion == null)
            {
                return NotFound( mensaje );
            }


            return Ok(new
            {
                Data = evaluacion,
                Mensaje = mensaje
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvaluacion([FromBody] AddEvaluacionDto evaluacionDto, int pacienteId)
        {


            var (exito, mensaje) = await _evaluacionService.AddAsync(evaluacionDto, pacienteId);

            if (!exito)
            {

                return BadRequest( mensaje );
            }


            return Ok(mensaje); 
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEvaluacion([FromBody] UpdateEvaluacionDto evaluacionDto, int pacienteId)
        {
            var (exito, mensaje) = await _evaluacionService.UpdateAsync(evaluacionDto, pacienteId);

            if (!exito)
            {

                return NotFound(mensaje);
            }

            return NoContent(); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvaluacion(int pacienteId)
        {
            var (exito, mensaje) = await _evaluacionService.DeleteAsync(pacienteId);

            if (!exito)
            {
                return NotFound(mensaje); 
            }

            return NoContent(); 
        }


        [HttpGet]
        [Route("/api/preguntas")] 
        public async Task<IActionResult> GetPreguntas()
        {
            var (preguntas, mensaje) = await _evaluacionService.GetPreguntasAsync();

            if (!preguntas.Any())
            {
                return NotFound(mensaje);
            }

            return Ok(preguntas);
        }
    }
}