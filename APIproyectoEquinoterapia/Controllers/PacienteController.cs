using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Services;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacientes()
        {
            var (pacientes,mensaje) = await _pacienteService.GetPacientesAsync();
            if (pacientes == null || !pacientes.Any())
                return NotFound(mensaje);
            return Ok(pacientes);
        }


        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(int PacienteId)
        {
            var (paciente,mensaje) = await _pacienteService.GetPacienteAsync(PacienteId);
            if (paciente == null)
                return NotFound(mensaje);
            return Ok(paciente);
        }


        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutPaciente(int pacienteId, PacienteDTO pacienteDTO)
        {

            var (respuesta,mensaje) = await _pacienteService.PutPacienteAsync(pacienteId, pacienteDTO);
            if (!respuesta)
                return NotFound(mensaje);

            return Ok(mensaje);
        }

        
        [HttpPost("agregar")]
        public async Task<IActionResult> PostPaciente(PacienteConArchivosDTO pacienteconarchivoDTO)
        {
            var (respuesta,mensaje) = await _pacienteService.PostPacienteAsync(pacienteconarchivoDTO);
            if (!respuesta)
                return Conflict(mensaje);

            return Ok(mensaje);
        }


        [HttpGet("obtener/archivo/{archivoRequerido}")]
        public async Task<IActionResult> GetArchivo(int pacienteId, string archivoRequerido)
        {
            var archivo = await _pacienteService.GetArchivoAsync(archivoRequerido,pacienteId);
            if (archivo == null || archivo.Length == 0)
                return NotFound("Archivo no encontrado.");
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
            return File(archivo, contentType);
        }

        [HttpPut("actualizar/archivo/{archivoRequerido}")]
        public async Task<IActionResult> PutArchivo(
            int pacienteId,
            string archivoRequerido,
            IFormFile nuevoArchivo)
        {
            var actualizado = await _pacienteService.PutArchivoAsync(pacienteId, archivoRequerido, nuevoArchivo);
            if (!actualizado)
                return NotFound("Archivo no encontrado.");
            return Ok("Archivo actualizado correctamente.");
        }

        [HttpDelete("eliminar/archivo/{achivoRequerido}")]
        public async Task<IActionResult> DeleteArchivo(int pacienteId, string archivoRequerido)
        {
            var eliminado = await _pacienteService.DeleteArchivoAsync(pacienteId, archivoRequerido);
            if (!eliminado)
                return NotFound("Paciente o archivo no encontrado.");
            return Ok("Archivo eliminado correctamente.");
        }


        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeletePaciente(int pacienteId)
        {
            var (respuesta,mensaje) = await _pacienteService.DeletePacienteAsync(pacienteId);
            if (!respuesta)
                return NotFound(mensaje);

            return Ok(mensaje);
        }

    }
}
