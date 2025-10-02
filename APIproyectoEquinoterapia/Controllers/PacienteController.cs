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
        public async Task<ActionResult<IEnumerable<PacienteConFaltasDTO>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetPacientesAsync();
            if (pacientes == null || !pacientes.Any())
                return NotFound("No hay pacientes registrados.");
            return Ok(pacientes);
        }


        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<PacienteConFaltasDTO>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteAsync(id);
            if (paciente == null)
                return NotFound("Paciente no encontrado.");
            return Ok(paciente);
        }


        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteDTO pacienteDTO)
        {
            if (id != pacienteDTO.PacienteId)
                return BadRequest();

            var actualizado = await _pacienteService.PutPacienteAsync(id, pacienteDTO);
            if (!actualizado)
                return NotFound("Paciente no encontrado.");

            return Ok("Paciente actualizado correctamente.");
        }

        
        [HttpPost("agregar")]
        public async Task<ActionResult<PacienteConArchivosDTO>> PostPaciente(PacienteConArchivosDTO pacienteconarchivoDTO)
        {
            var agregado = await _pacienteService.PostPacienteAsync(pacienteconarchivoDTO);
            if (!agregado)
                return Conflict("Paciente ya existe.");

            return Ok(pacienteconarchivoDTO);
        }

        
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var eliminado = await _pacienteService.DeletePacienteAsync(id);
            if (!eliminado)
                return NotFound("Paciente no encontrado.");

            return Ok("Paciente eliminado correctamente.");
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

    }
}
