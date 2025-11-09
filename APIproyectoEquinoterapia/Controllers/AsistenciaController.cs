using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/asistencias")] 
    public class AsistenciasController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciasController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsistenciasPorSesion([FromQuery] int sesionId)
        {
            var (asistencias, mensaje) = await _asistenciaService.GetAsync(sesionId);

            if (!asistencias.Any() && mensaje.Contains("No se encontraron"))
            {
                return Ok(asistencias); 
            }
            if (!asistencias.Any())
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return Ok(asistencias);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsistencias([FromBody] AsistenciaAddDto asistenciaDto)
        {
            var (exito, mensaje) = await _asistenciaService.AddAsync(asistenciaDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje });
            }

            return Ok(new { Mensaje = mensaje }); 
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsistencias([FromBody] AsistenciaUpdateDto asistenciaUpdateDto)
        {
            var (mensaje, exito) = await _asistenciaService.UpdateAsync(asistenciaUpdateDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje }); 
            }
            return Ok(new { Mensaje = mensaje }); 
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsistencias([FromBody] List<int> asistenciaIds)
        {
            var (mensaje, exito) = await _asistenciaService.DeleteAsync(asistenciaIds);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje });
            }
            return Ok(new { Mensaje = mensaje }); 
        }

    }
}