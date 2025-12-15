using Equinoterapia.Application.DTOs.Caballos;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCaballoDto caballoDto)
        {

            var (exito, mensaje) = await _caballoService.UpdatelAsync(caballoDto, id);

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