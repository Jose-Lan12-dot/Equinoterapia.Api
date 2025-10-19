using APIproyectoEquinoterapia.Data.Services.Paciente_;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
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
            var (archivo,mensaje,tipo) = await _archivoService.GetArchivoAsync(archivoRequerido, pacienteId);
            if (archivo == null || archivo.Length == 0)
                return NotFound(mensaje);

            string contentType = tipo.ToLower() switch
            {
                "pdf" => "application/pdf",
                "jpg" => "image/jpeg",
                _ => "application/octet-stream"
            };
            return Ok(File(archivo, contentType));
        }

        [HttpPut("actualizar/{archivoRequerido}")]
        public async Task<IActionResult> PutArchivo(ArchivoDTO archivoDTO)
        {
            var (exito,mensaje) = await _archivoService.PutArchivoAsync(archivoDTO);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }


        [HttpPost("agregar")]
        public async Task<IActionResult> PostArchivo(ArchivoDTO archivoDTO)
        {
            var (exito,mensaje) = await _archivoService.PostArchivoAsync(archivoDTO);
            if (!exito)
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
