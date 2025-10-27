using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly SesionService _sesionService;

        public SesionController(SesionService sesionService)
        {
            _sesionService = sesionService;
        }

        [HttpGet("obtener-todas")]
        public async Task<IActionResult> GetAll()
        {
            var (sesiones, mensaje) = await _sesionService.GetAllAsync();
            if (!sesiones.Any())
                return NotFound(mensaje);
            return Ok(new { mensaje, sesiones });
        }

        [HttpGet("obtener-por-semana")]
        public async Task<IActionResult> GetByWeek([FromQuery] DateOnly startOfWeek)
        {
            var (sesiones, mensaje) = await _sesionService.GetByWeekAsync(startOfWeek);
            if (!sesiones.Any())
                return NotFound(mensaje);
            return Ok(new { mensaje, sesiones });
        }

        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (sesion, mensaje) = await _sesionService.GetByIdAsync(id);
            if (sesion == null)
                return NotFound(mensaje);
            return Ok(new { mensaje, sesion });
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> Add([FromBody] AddSesionDto addSesionDto)
        {
            var (exito, mensaje) = await _sesionService.AddAsync(addSesionDto);
            if (!exito)
                return Conflict(mensaje);
            return Ok(mensaje);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SesionDto sesionDto)
        {
            var (exito, mensaje) = await _sesionService.UpdateAsync(sesionDto, id);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _sesionService.DeleteAsync(id);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }
    }
}
