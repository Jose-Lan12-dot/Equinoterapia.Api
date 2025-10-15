using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        
        private readonly ArchivoService _archivoService;
        public ArchivoController(ArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [HttpGet("obtener/{archivoRequerido}")]
        public async Task<IActionResult> GetArchivo(int pacienteId, string archivoRequerido)
        {
            var (archivo,mensaje) = await _archivoService.GetArchivoAsync(archivoRequerido, pacienteId);
            if (archivo == null || archivo.Length == 0)
                return NotFound(mensaje);

            string contentType = archivoRequerido.ToLower() switch
            {
                "diagnosticomedico" => "application/pdf",
                "certificadomedico" => "application/pdf",
                "actanacimiento" => "application/pdf",
                "curp" => "application/pdf",
                "comprobantedomicilio" => "application/pdf",
                "inetutor" => "application/pdf",
                "fotografia" => "image/jpeg",
                _ => "application/octet-stream"
            };
            return Ok(File(archivo, contentType));
        }

        [HttpPut("actualizar/{archivoRequerido}")]
        public async Task<IActionResult> PutArchivo(
            int pacienteId,
            string archivoRequerido,
            IFormFile nuevoArchivo)
        {
            var (respuesta,mensaje) = await _archivoService.PutArchivoAsync(pacienteId, archivoRequerido, nuevoArchivo);
            if (!respuesta)
                return NotFound(mensaje);
            return Ok(mensaje);
        }


        [HttpPost("agregar")]
        public async Task<IActionResult> PostArchivo(int pacienteId,ArchivoDTO archivoDTO)
        {
            var (respuesta,mensaje) = await _archivoService.PostArchivoAsync(archivoDTO,pacienteId);
            if (!respuesta)
                return NotFound(mensaje);
            return Ok(mensaje);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> DeleteArchivo(int pacienteId, string archivoRequerido)
        {
            var (respuesta,mensaje) = await _archivoService.DeleteArchivoAsync(pacienteId, archivoRequerido);
            if (!respuesta)
                return NotFound(mensaje);
            return Ok(mensaje);
        }


    }
}
