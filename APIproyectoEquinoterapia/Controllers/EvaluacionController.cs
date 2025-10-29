using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
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

//////////////////////
        [HttpGet("obtener/{pacienteId}")]
        public async Task<ActionResult<EvaluacionDto>> Get(int pacienteId)
        {
            var (evaluacion,nombrePaciente,mensaje) = await _EvaluacionService.GetAsync(pacienteId);
            if (evaluacion == null)
            {
                return BadRequest(mensaje);
            }

            return Ok(new{evaluacion, nombrePaciente, mensaje});
        }

//////////////////////
        [HttpGet("obtener/preguntas")]
        public async Task<ActionResult<PreguntasDto>> GetPreguntas()
        {
            var (preguntas,mensaje) = await _EvaluacionService.GetPreguntasAsync();
            if (!preguntas.Any())
            {
                return BadRequest(mensaje);
            }

            return Ok(new{mensaje, preguntas});
        }

//////////////////////
        [HttpPost("agregar")]
        public async Task<IActionResult> Add(EvaluacionDto evaluacionDto, int pacienteId)
        {
            var (exito,mensaje) = await _EvaluacionService.AddAsync(evaluacionDto, pacienteId);
            
            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

//////////////////////
        [HttpPut("actualizar/{pacienteId}")]
        public async Task<IActionResult> Update(EvaluacionDto evaluacionDto, int pacienteId)
        {
            var (exito, mensaje) = await _EvaluacionService.UpdateAsync(evaluacionDto, pacienteId);
            if (!exito)
            {
                return Conflict(mensaje);
            }

            return Ok(mensaje);
        }

//////////////////////
        [HttpDelete("eliminar/{pacienteId}")]
        public async Task<IActionResult> DeleteEvaluacion(int pacienteId)
        {
            var (exito,mensaje) = await _EvaluacionService.DeleteAsync(pacienteId);
            if (!exito)
            {
                return BadRequest(mensaje);
            }
            return Ok(mensaje);
        }


    }
}
