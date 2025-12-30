using Equinoterapia.Application.DTOs.Archivos;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [Authorize]
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
            var (archivo, mensaje) = await _archivoService.GetAsync(nombreArchivo, pacienteId);

            if (archivo.Archivo == null)
            {
                return NotFound(mensaje);
            }
            string contentType = archivo.Extension.ToLower() switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                _ => "application/octet-stream"
            };

            return File(archivo.Archivo,contentType) ;
        }


        [HttpGet("/api/archivos/obtener-tipoarchivo")]
        public async Task<IActionResult> GetTipoArchivo()
        {
            var tiposArchivos = await _archivoService.GetTipoArchivo();

            return Ok(tiposArchivos);
        }

        [HttpPost]
        public async Task<IActionResult> SubirArchivo(int pacienteId, AddArchivoDto archivoDto)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); 
            //}

            var (exito, mensaje) = await _archivoService.AddAsync(archivoDto, pacienteId);

            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(new { Mensaje = mensaje });
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarArchivo(int pacienteId, UpdateArchivoDto archivoDto)
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