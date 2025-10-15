using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace APIproyectoEquinoterapia.Controllers
{
    [Authorize]
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
            var (caballos,mensaje) = await _caballoService.GetCaballosAsync();

            if (caballos == null || !caballos.Any())
                
                return NotFound(mensaje);

            return Ok(caballos);
        }

        
        [HttpGet("obtener/{placa}")]
        public async Task<ActionResult<CaballoDTO>> GetCaballo(string placa)
        {
            var (caballo, mensaje) = await _caballoService.GetCaballoAsync(placa);

            if (caballo == null)
            {
                return NotFound(mensaje);
            }

            return Ok(caballo);
        }

        
        [HttpPut("actualizar/{placa}")]
        public async Task<IActionResult> PutCaballo(string placa, CaballoDTO caballoDTO)
        {
           
            if (placa != caballoDTO.Placa)
            {
                return BadRequest();
            }

            var (respuesta, mensaje) = await _caballoService.PutCaballoAsync(placa, caballoDTO);

            if (!respuesta) 
            {
                return NotFound(mensaje);
            }
            return Ok(mensaje);
        }

        
        [HttpPost("agregar")]
        public async Task<ActionResult<CaballoDTO>> PostCaballo(CaballoDTO caballoDTO)
        {

            var (respuesta, mensaje) = await _caballoService.PostCaballoAsync(caballoDTO);
            if (!respuesta)
            {
                return  NotFound(mensaje);
            }

            return Ok(mensaje);
        }

        
        //[HttpDelete("eliminar/{placa}")]
        //public async Task<IActionResult> DeleteCaballo(string placa)
        //{
        //    var (respuesta,mensaje) = await _caballoService.DeleteCaballoAsync(placa);
        //    if (!respuesta)
        //    {
        //        return NotFound(mensaje);
        //    }

        //    return Ok(mensaje);
        //}
    }
}
