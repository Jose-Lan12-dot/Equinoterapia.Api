using APIproyectoEquinoterapia.Data.Services.Paciente_;
using APIproyectoEquinoterapia.DTOs.Evaluacion_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionController : ControllerBase
    {
        private readonly EvaluacionService _EvaluacionService;

        public EvaluacionController(EvaluacionService evaluacionService)
        {
            _EvaluacionService = evaluacionService;
        }

        [HttpGet("obtener/{pacienteId}")]
        public async Task<ActionResult<EvaluacionDTO>> GetEvaluacion(int pacienteId)
        {
            var (evaluacion,mensaje) = await _EvaluacionService.GetEvaluacionAsync(pacienteId);
            if (evaluacion == null)
            {
                return BadRequest(mensaje);
            }

            return Ok(new{mensaje, evaluacion});
        }

        [HttpGet("obtener/preguntas")]
        public async Task<ActionResult<PreguntasDTO>> GetPreguntas()
        {
            var (preguntas,mensaje) = await _EvaluacionService.GetPreguntasAsync();
            if (!preguntas.Any())
            {
                return BadRequest(mensaje);
            }

            return Ok(new{mensaje, preguntas});
        }


        [HttpPut("actualizar/{pacienteId}")]
        public async Task<IActionResult> PutEvaluacion(EvaluacionDTO evaluacionDTO, int pacienteId)
        {
            var (exito,mensaje) = await _EvaluacionService.PutEvaluacionAsync(evaluacionDTO, pacienteId);
            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> PostEvaluacion(EvaluacionDTO evaluacionDTO, int pacienteId)
        {
            var (exito,mensaje) = await _EvaluacionService.PostEvaluacionAsync(evaluacionDTO, pacienteId);
            
            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        [HttpDelete("eliminar/{pacienteId}")]
        public async Task<IActionResult> DeleteEvaluacion(int pacienteId)
        {
            var (exito,mensaje) = await _EvaluacionService.DeleteEvaluacionAsync(pacienteId);
            if (!exito)
            {
                return BadRequest(mensaje);
            }
            return Ok(mensaje);
        }


    }
}
