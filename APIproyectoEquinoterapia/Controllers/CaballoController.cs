using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using APIproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Mvc;


namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaballoController : ControllerBase
    {
        private readonly CaballoService _caballoService;


        public CaballoController(CaballoService caballoservice)
        {
            _caballoService = caballoservice;
        }

        
        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<CaballoDTO>>> GetCaballos()
        {
            var caballos = await _caballoService.GetCaballosAsync();

            if (caballos == null || !caballos.Any())
                
                return NotFound("No hay ningun elemento.");

            return Ok(caballos);
        }

        
        [HttpGet("obtener/{placa}")]
        public async Task<ActionResult<CaballoDTO>> GetCaballo(string placa)
        {
            var caballo = await _caballoService.GetCaballoAsync(placa);

            if (caballo == null)
            {
                return NotFound("Caballo no encontrado");
            }

            return caballo;
        }

        
        [HttpPut("actualizar/{placa}")]
        public async Task<IActionResult> PutCaballo(string placa, CaballoDTO caballoDTO)
        {
           
            if (placa != caballoDTO.Placa)
            {
                return BadRequest();
            }

            var respuesta = await _caballoService.PutCaballoAsync(placa, caballoDTO);

            if (respuesta) 
            {
                return NotFound("Caballo no encontrado");
            }
            return Ok("Caballo actualizado correctamente");
        }

        
        [HttpPost("agregar")]
        public async Task<ActionResult<CaballoDTO>> PostCaballo(CaballoDTO caballoDTO)
        {

            var respuesta = await _caballoService.PostCaballoAsync(caballoDTO);
            if (respuesta)
            {
                return  NotFound("Caballo ya existe");
            }

            return Ok("Agreagdo exitosamnete");
        }

        
        [HttpDelete("eliminar/{placa}")]
        public async Task<IActionResult> DeleteCaballo(string placa)
        {
            var respuesta = await _caballoService.DeleteCaballoAsync(placa);
            if (respuesta)
            {
                return NotFound("Caballo no encontrado");
            }

            return Ok("Eliminado");
        }
    }
}
