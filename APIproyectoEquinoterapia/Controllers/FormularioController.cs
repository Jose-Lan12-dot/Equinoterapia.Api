using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Data.Services;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioController : ControllerBase
    {
        private readonly FormularioService _formularioService;

        public FormularioController(FormularioService formularioService)
        {
            _formularioService = formularioService;
        }

        // GET: api/Formulario
        [HttpGet("obtener")]
        public async Task<ActionResult<FormularioDTO>> Getformulario(int pacienteId)
        {
            var (formulario,mensaje) = await _formularioService.GetFormularioAsync(pacienteId);
            if (formulario == null)
            {
                return BadRequest(mensaje);
            }

            return Ok(formulario);
        }

        // GET: api/Formulario
        [HttpGet("obtener/preguntas")]
        public async Task<ActionResult<FormularioDTO>> GetPreguntas()
        {
            var (preguntas,mensaje) = await _formularioService.GetPreguntasAsync();
            if (!preguntas.Any())
            {
                return BadRequest(mensaje);
            }

            return Ok(preguntas);
        }


        // PUT: api/Formulario/5
        [HttpPut("actualizar")]
        public async Task<IActionResult> PutFormulario(FormularioDTO formularioDTO, int pacienteId)
        {
            var (respuesta,mensaje) = await _formularioService.PutFormularioAsync(formularioDTO, pacienteId);
            if (!respuesta)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        // POST: api/Formulario
        [HttpPost("agregar")]
        public async Task<IActionResult> PostFormulario(FormularioDTO formularioDTO, int pacienteId)
        {
            var (respuesta,mensaje) = await _formularioService.PostFormularioAsync(formularioDTO, pacienteId);
            
            if (!respuesta)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        // DELETE: api/Formulario/5
        [HttpDelete("eliminar")]
        public async Task<IActionResult> DeleteFormulario(int id)
        {
            var (respuesta,mensaje) = await _formularioService.DeleteFormularioAsync(id);
            if (!respuesta)
            {
                return BadRequest(mensaje);
            }
            return Ok(mensaje);
        }


    }
}
