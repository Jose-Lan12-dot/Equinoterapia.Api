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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly CitaService _citaService;

        public CitaController(CitaService citaService)
        {
            _citaService = citaService;
        }

        
        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<CitaDTO>>> GetCita()
        {
            var citas = await _citaService.GetCitasAsync();
            if (citas == null || !citas.Any())
                return NotFound("No se encontraron citas.");
            return Ok(citas);
        }

        
        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<CitaDTO>> GetCita(int id)
        {
            var cita = await _citaService.GetCitaAsync(id);
            if (cita == null)
                return NotFound("Cita no encontrada");
            return Ok(cita);
        }

        
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutCita(int id, CitaDTO citaDTO)
        {
            var actualizado = await _citaService.PutCitaAsync(id, citaDTO);
            if (!actualizado)
                return NotFound();
            return Ok("Cita actualizada correctamente.");
        }

        
        [HttpPost("agregar")]
        public async Task<ActionResult<CitaDTO>> PostCita(CitaDTO citaDTO)
        {
            var agregado = await _citaService.PostCitaAsync(citaDTO);
            if (!agregado)
                return NotFound("Sesion o Paciente_ no existe");
            return Ok(citaDTO);
        }

        
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var eliminado = await _citaService.DeleteCitaAsync(id);
            if (!eliminado)
                return NotFound();
            return Ok("Cita eliminada correctamente.");
        }
    }
}
