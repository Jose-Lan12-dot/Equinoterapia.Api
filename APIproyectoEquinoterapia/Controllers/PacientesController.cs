using Equinoterapia.Application.DTOs.Pacientes; 
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Equinoterapia.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }



        ////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (pacientes, mensaje) = await _pacienteService.GetAllAsync();

            if (!pacientes.Any())
            {
                return NotFound(mensaje);
            }

            return Ok(new { Message = mensaje, Data = pacientes });
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (datosPaciente, mensaje) = await _pacienteService.GetByIdAsync(id);

            if (datosPaciente == null)
            {
                return NotFound(mensaje);
            }

            return Ok(new { Message = mensaje, Data = datosPaciente });
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPacienteConArchivosDto addPacienteDto)
        {
            var (exito, mensaje) = await _pacienteService.AddAsync(addPacienteDto);

            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(mensaje);
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePacienteDto pacienteDto)
        {
            if (id != pacienteDto.PacienteId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _pacienteService.UpdateAsync(pacienteDto, id);

            if (!exito)
            {
                return NotFound(mensaje);
            }

            return NoContent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _pacienteService.DeleteAsync(id);

            if (!exito)
            {
                return NotFound(mensaje);
            }

            return Ok(mensaje);
        }

        [HttpPatch("baja/{id}")]
        public async Task<IActionResult> DarDeBaja(int id)
        {
            var (exito, mensaje) = await _pacienteService.SetBajaAsync(id);
            if (!exito)
            {
                return NotFound( mensaje );
            }

            return Ok( mensaje );
        }

        [HttpPatch("reactivar/{id}")]
        public async Task<IActionResult> Reactivar(int id)
        {
            var (exito, mensaje) = await _pacienteService.SetReactivar(id);
            if (!exito)
            {
                return NotFound( mensaje );
            }

            return Ok( mensaje );
        }
    }
}