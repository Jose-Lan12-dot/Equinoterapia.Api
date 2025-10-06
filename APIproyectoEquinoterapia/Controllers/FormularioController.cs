using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIproyectoEquinoterapia.Models;
using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Services;

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
        [HttpGet("/obtener")]
        public async Task<ActionResult<FormularioDTO>> Getformulario(int pacienteId)
        {
            var (formulario,mensaje) = await _formularioService.GetFormularioAsync(pacienteId);
            if (formulario == null)
            {
                return BadRequest(mensaje);
            }

            return Ok(formulario);
        }


        //// PUT: api/Formulario/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFormulario(int id, Formulario formulario)
        //{
        //    if (id != formulario.FormularioId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(formulario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FormularioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Formulario
        [HttpPost("/agregar")]
        public async Task<IActionResult> PostFormulario(FormularioDTO formularioDTO, int pacienteId)
        {
            var (respuesta,mensaje) = await _formularioService.PostFormularioAsync(formularioDTO, pacienteId);
            
            if (!respuesta)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        //// DELETE: api/Formulario/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFormulario(int id)
        //{
        //    var formulario = await _context.formulario.FindAsync(id);
        //    if (formulario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.formulario.Remove(formulario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool FormularioExists(int id)
        //{
        //    return _context.formulario.Any(e => e.FormularioId == id);
        //}
    }
}
