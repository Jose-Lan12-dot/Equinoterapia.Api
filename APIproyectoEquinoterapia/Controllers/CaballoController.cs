using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CaballoController : ControllerBase
    {
        private readonly CaballoService _caballoService;


        public CaballoController(CaballoService caballoservice)
        {
            _caballoService = caballoservice;
        }

//////////////////////
        [HttpGet("obtener-todos")]
        public async Task<ActionResult<IEnumerable<CaballoDto>>> GetAll()
        {
            var (caballos, mensaje) = await _caballoService.GetAllAsync();

            if (caballos.Count() == 0)

                return NotFound(mensaje);

            return Ok(new {mensaje, caballos});
        }

 //////////////////////
        [HttpGet("obtener-por-id/{caballoId}")]
        public async Task<ActionResult<CaballoDto>> GetById(int caballoId)
        {
            var (caballo, mensaje) = await _caballoService.GetByIdAsync(caballoId);

            if (caballo == null)
            {
                return NotFound(mensaje);
            }

            return Ok(new {mensaje,caballo });
        }

//////////////////////
        [HttpPost("agregar")]
        public async Task<IActionResult> Create(CaballoCreationDto caballoCreationDto)
        {

            var (exito, mensaje) = await _caballoService.CreateAsync(caballoCreationDto);

            if (!exito)
            {
                return Conflict(mensaje);
            }
            return Ok(mensaje);
        }

//////////////////////
        [HttpPut("actualizar-todo/{caballoId}")]
        public async Task<ActionResult<CaballoDto>> UpdateAll(CaballoDto caballoDto, int caballoId)
        {

            var (exito, mensaje) = await _caballoService.UpdateAllAsync(caballoDto,caballoId);
            if (!exito)
            {
                return NotFound(mensaje);
            }

            return Ok(mensaje);
        }

//////////////////////
        [HttpPatch("actualizar-parcialmente/{caballoId}")]
        public async Task<ActionResult<CaballoDto>> UpdatePartial(CaballoDto caballoDto, int caballoId)
        {

            var (exito, mensaje) = await _caballoService.UpdatePartialAsync(caballoDto, caballoId);
            if (!exito)
            {
                return NotFound(mensaje);
            }

            return Ok(mensaje);
        }

        //////////////////////
        [HttpDelete("eliminar/{caballoId}")]
        public async Task<IActionResult> Delete(int caballoId)
        {
            var (exito, mensaje) = await _caballoService.DeleteAsync(caballoId);
            if (!exito)
            {
                return NotFound(mensaje);
            }
            return Ok(mensaje);
        }

    }
}
