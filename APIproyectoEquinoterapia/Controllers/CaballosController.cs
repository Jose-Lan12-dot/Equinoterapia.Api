using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaballosController : ControllerBase
    {
        private readonly ICaballoService _caballoService;

        public CaballosController(ICaballoService caballoService)
        {
            _caballoService = caballoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (caballos, mensaje) = await _caballoService.GetAllAsync();

            if (!caballos.Any())
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return Ok(caballos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (caballo, mensaje) = await _caballoService.GetByIdAsync(id);

            if (caballo == null)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return Ok(caballo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCaballoDto addCaballoDto)
        {

            var (exito, mensaje) = await _caballoService.AddAsync(addCaballoDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje }); 
            }

            return Ok(new { Mensaje = mensaje });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] CaballoDto caballoDto)
        {
            if (id != caballoDto.CaballoId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _caballoService.UpdatePartialAsync(caballoDto, id);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return NoContent(); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _caballoService.DeleteAsync(id);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return NoContent(); 
        }
    }
}