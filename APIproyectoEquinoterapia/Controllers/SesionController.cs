using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SesionesController : ControllerBase
    {
        private readonly ISesionService _sesionService;

        public SesionesController(ISesionService sesionService)
        {
            _sesionService = sesionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (sesiones, mensaje) = await _sesionService.GetAllAsync();
            if (!sesiones.Any())
            {
                return NotFound(new { Mensaje = mensaje }); 
            }
            return Ok(sesiones); 
        }


        [HttpGet("by-week")]
        public async Task<IActionResult> GetByWeek([FromQuery] DateOnly startOfWeek)
        {
            var (sesiones, mensaje) = await _sesionService.GetByWeekAsync(startOfWeek);
            if (!sesiones.Any())
            {
                return NotFound(new { Mensaje = mensaje }); 
            }
            return Ok(sesiones); 
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (sesion, mensaje) = await _sesionService.GetByIdAsync(id);
            if (sesion == null)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }
            return Ok(sesion); 
        }

        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var formData = await _sesionService.GetFormDataAsync();

            return Ok(formData);
        }

        [HttpGet("horarios-ocupados")]
        public async Task<IActionResult> GetHorariosOcupados([FromQuery] DateOnly fecha)
        {
            var horarios = await _sesionService.GetHorariosOcupadosAsync(fecha);

            return Ok(horarios);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSesionDto addSesionDto)
        {
            var (exito, mensaje) = await _sesionService.AddAsync(addSesionDto);
            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje }); // 400
            }
            return Ok(new { Mensaje = mensaje });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SesionDto sesionDto)
        {
            if (id != sesionDto.SesionId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _sesionService.UpdateAsync(sesionDto, id);
            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _sesionService.DeleteAsync(id);
            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje });
            }
            return NoContent(); 
        }


    }
}