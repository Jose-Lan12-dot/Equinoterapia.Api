using APIproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("eliminar/{achivoRequerido}")]
        public async Task<IActionResult> DeleteArchivo(int pacienteId, string archivoRequerido)
        {
            var (respuesta,mensaje) = await _archivoService.DeleteArchivoAsync(pacienteId, archivoRequerido);
            if (!respuesta)
                return NotFound(mensaje);
            return Ok(mensaje);
        }


    }
}
