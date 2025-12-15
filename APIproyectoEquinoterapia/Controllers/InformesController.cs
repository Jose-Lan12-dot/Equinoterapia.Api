using Equinoterapia.Application.DTOs.Informes;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class InformesController : ControllerBase
    {
        private readonly IInformeService _informeService;
        private readonly IInformeExportService _exportService;

        public InformesController(IInformeService informeService, IInformeExportService exportService)
        {
            _informeService = informeService;
            _exportService = exportService;
        }

        // GET: api/informes?mes=9&anio=2025
        [HttpGet]
        public async Task<IActionResult> GetInforme([FromQuery] int mes, [FromQuery] int anio)
        {
            var informe = await _informeService.GetInformeAsync(mes, anio);
            return Ok(informe);
        }

        // GET: api/reportes/estadisticas?mes=9&anio=2025
        [HttpGet("estadisticas")]
        public async Task<IActionResult> GetEstadisticas([FromQuery] int mes, [FromQuery] int anio)
        {
            var stats = await _informeService.GetEstadisticasAsync(mes, anio);
            return Ok(stats);
        }

        // POST: api/reportes
        [HttpPost]
        public async Task<IActionResult> GuardarInforme([FromBody] InformeDto informeDto)
        {

            var (exito, mensaje) = await _informeService.GuardarInformeAsync(informeDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje });
            }

            return Ok(new { Mensaje = mensaje });
        }

        [HttpGet("download/pdf")]
        public async Task<IActionResult> DescargarPdf(
        [FromQuery] int mes,
        [FromQuery] int anio)
        {
            var informe = await _informeService.GetInformeAsync(mes, anio);
            var archivoBytes = _exportService.GenerarPdf(informe);

            var nombreArchivo = $"Informe_{mes}_{anio}.pdf";
            return File(archivoBytes, "application/pdf", nombreArchivo);
        }

        [HttpGet("download/excel")]
        public async Task<IActionResult> DescargarExcel(
            [FromQuery] int mes,
            [FromQuery] int anio) 
        {
            var informe = await _informeService.GetInformeAsync(mes, anio);

            var archivoBytes = _exportService.GenerarExcel(informe);

            var nombreArchivo = $"Informe_{mes}_{anio}.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(archivoBytes, contentType, nombreArchivo);
        }


    }
}