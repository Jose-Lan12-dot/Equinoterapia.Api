using APIproyectoEquinoterapia.Data.Services;
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


        [HttpGet("obtener/{nombreArchivo}")]
        public async Task<IActionResult> Get(int pacienteId, string nombreArchivo)
        {
            var (archivo, mensaje, tipo) = await _archivoService.Getsync(nombreArchivo, pacienteId);
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

        [HttpPost("agregar")]
        public async Task<IActionResult> Add(ArchivoDto archivoDTO, int pacienteId)
        {
            var (exito, mensaje) = await _archivoService.AddAsync(archivoDTO, pacienteId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }

        [HttpPut("actualizar/{pacienteId}")]
        public async Task<IActionResult> Update(ArchivoDto archivoDTO, int pacienteId)
        {
            var (exito, mensaje) = await _archivoService.UpdateAsync(archivoDTO, pacienteId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }

        [HttpDelete("eliminar/{pacienteId}")]
        public async Task<IActionResult> Delete(int pacienteId, string archivoRequerido)
        {
            var (exito,mensaje) = await _archivoService.DeleteAsync(archivoRequerido, pacienteId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }


    }
}
