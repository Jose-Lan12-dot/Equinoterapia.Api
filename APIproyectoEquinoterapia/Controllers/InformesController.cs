using Equinoterapia.Application.DTOs.Informes;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InformesController : ControllerBase
    {
        private readonly IInformeService _reporteService;

        public InformesController(IInformeService reporteService)
        {
            _reporteService = reporteService;
        }

        // GET: api/informes?mes=9&anio=2025
        [HttpGet]
        public async Task<IActionResult> GetInforme([FromQuery] int mes, [FromQuery] int anio)
        {
            var informe = await _reporteService.GetInformeAsync(mes, anio);
            return Ok(informe);
        }

        // GET: api/reportes/estadisticas?mes=9&anio=2025
        [HttpGet("estadisticas")]
        public async Task<IActionResult> GetEstadisticas([FromQuery] int mes, [FromQuery] int anio)
        {
            var stats = await _reporteService.GetEstadisticasAsync(mes, anio);
            return Ok(stats);
        }

        // POST: api/reportes
        [HttpPost]
        public async Task<IActionResult> GuardarInforme([FromBody] InformeDto informeDto)
        {

            var (exito, mensaje) = await _reporteService.GuardarInformeAsync(informeDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje });
            }

            return Ok(new { Mensaje = mensaje });
        }
    }
}