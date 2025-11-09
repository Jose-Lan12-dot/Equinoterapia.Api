using Equinoterapia.Application.DTOs; 
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] 
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (paciente, faltas, eval, archivos, mensaje) = await _pacienteService.GetByIdAsync(id);

            if (paciente == null)
            {
                return NotFound(mensaje);
            }
            var datos = new
            {
                paciente,
                numeroFaltas = faltas,
                faltaEvaluacion = eval,
                archivosPendientes = archivos
            };

            return Ok(new { Message = mensaje, Data = datos });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPacienteConArchivosDto addPacienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (exito, mensaje) = await _pacienteService.AddAsync(addPacienteDto);

            if (!exito)
            {
                return BadRequest(mensaje);
            }

            return Ok(new { Mensaje = mensaje });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.PacienteId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (exito, mensaje) = await _pacienteService.UpdateAllAsync(pacienteDto, id);

            if (!exito)
            {
                return NotFound(mensaje); 
            }

            return NoContent(); 
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.PacienteId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _pacienteService.UpdatePartialAsync(pacienteDto, id);

            if (!exito)
            {
                return NotFound(mensaje); 
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _pacienteService.DeleteAsync(id);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje });
            }

            return NoContent(); 
        }
    }
}