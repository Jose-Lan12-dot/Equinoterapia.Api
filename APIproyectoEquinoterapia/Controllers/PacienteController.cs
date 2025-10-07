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
        public async Task<ActionResult<ArchivoDTO>> PostPaciente(PacienteDTO pacienteDTO)
        {
            var (respuesta,mensaje) = await _pacienteService.PostPacienteAsync(pacienteDTO);
            if (!respuesta)
                return Conflict(mensaje);

            return Ok(mensaje);
        }

        //[HttpDelete("eliminar/{id}")]
        //public async Task<IActionResult> DeletePaciente(int pacienteId)
        //{
        //    var (respuesta, mensaje) = await _pacienteService.DeletePacienteAsync(pacienteId);
        //    if (!respuesta)
        //        return NotFound(mensaje);

        //    return Ok(mensaje);
        //}


    }
}
