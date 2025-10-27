using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly AsistenciaService _asistenciaService;

        public AsistenciaController(AsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpGet("obtener-por-sesion/{sesionId}")]
        public async Task<IActionResult> GetBySesion(int sesionId)
        {
            var (asistencias, mensaje, exito) = await _asistenciaService.GetAsync(sesionId);
            if (!exito)
                return NotFound(mensaje);

            return Ok(new { mensaje, asistencias });
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> Add(AsistenciaAddDto asistenciaAddDto)
        {
            var (mensaje, exito) = await _asistenciaService.AddAsync(asistenciaAddDto);
            if (!exito)
                return Conflict(mensaje);

            return Ok(mensaje);
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Update(AsistenciaUpdateDto asistenciaUpdateDto)
        {
            var (mensaje, exito) = await _asistenciaService.UpdateAsync(asistenciaUpdateDto);
            if (!exito)
                return NotFound(mensaje);

            return Ok(mensaje);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(List<int> asistenciaIds)
        {
            var (mensaje, exito) = await _asistenciaService.DelateAsync(asistenciaIds);
            if (!exito)
                return NotFound(mensaje);

            return Ok(mensaje);
        }
    }
}
