using Equinoterapia.Application.DTOs.Archivos;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/pacientes/{pacienteId}/archivos")]
    public class ArchivosController : ControllerBase
    {
        private readonly IArchivoService _archivoService;

        public ArchivosController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [HttpGet("{nombreArchivo}")]
        public async Task<IActionResult> GetArchivo(int pacienteId, string nombreArchivo)
        {
            var (bytes, mensaje, extension) = await _archivoService.GetAsync(nombreArchivo, pacienteId);

            if (bytes == null)
            {
                return NotFound(mensaje);
            }
            var contentType = "application/octet-stream";

            return File(bytes, contentType, nombreArchivo + extension);
        }

        [HttpPost]
        public async Task<IActionResult> SubirArchivo(int pacienteId, [FromForm] AddArchivoDto archivoDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var (exito, mensaje) = await _archivoService.AddAsync(archivoDto, pacienteId);

            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(new { Mensaje = mensaje });
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarArchivo(int pacienteId, [FromForm] UpdateArchivoDto archivoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (exito, mensaje) = await _archivoService.UpdateAsync(archivoDto, pacienteId);

            if (!exito)
            {
                return NotFound(mensaje); 
            }

            return NoContent(); 
        }

        [HttpDelete("{nombreArchivo}")]
        public async Task<IActionResult> EliminarArchivo(int pacienteId, string nombreArchivo)
        {
            var (exito, mensaje) = await _archivoService.DeleteAsync(nombreArchivo, pacienteId);

            if (!exito)
            {
                return NotFound(mensaje);
            }

            return NoContent();
        }
    }
}