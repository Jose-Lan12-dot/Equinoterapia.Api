using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIproyectoEquinoterapia.Data.Services;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly SesionService _sesionService;

        public SesionController(SesionService sesionService)
        {
            _sesionService = sesionService;
        }

        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<SesionDTO>>> GetSesiones()
        {
            var sesiones = await _sesionService.GetSesionesAsync();
            if (sesiones == null || !sesiones.Any())
                return NotFound("No hay sesiones registradas.");
            return Ok(sesiones);
        }

        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<SesionDTO>> GetSesion(int id)
        {
            var sesion = await _sesionService.GetSesionAsync(id);
            if (sesion == null)
                return NotFound("Sesión no encontrada.");
            return Ok(sesion);
        }

        [HttpPost("agregar")]
        public async Task<ActionResult<SesionDTO>> PostSesion(SesionDTO sesionDTO)
        {
            await _sesionService.PostSesionAsync(sesionDTO);
            return Ok(sesionDTO);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutSesion(int id, SesionDTO sesionDTO)
        {
            if (id != sesionDTO.SesionId)
                return BadRequest("El ID de la sesión no coincide.");

            var actualizado = await _sesionService.PutSesionAsync(id, sesionDTO);
            if (!actualizado)
                return NotFound("Sesión no encontrada.");

            return Ok("Sesión actualizada correctamente.");
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var eliminado = await _sesionService.DeleteSesionAsync(id);
            if (!eliminado)
                return NotFound("Sesión no encontrada.");

            return Ok("Sesión eliminada correctamente.");
        }
    }
}
