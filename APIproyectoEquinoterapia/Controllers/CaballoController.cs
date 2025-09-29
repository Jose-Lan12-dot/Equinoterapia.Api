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
        private readonly caballoService _caballoService;


        public CaballoController(AppDbContext context, caballoService caballoservice)
        {
            _caballoService = caballoservice;
        }

        // GET: api/Caballo
        [HttpGet("obtenerCaballos")]
        public async Task<ActionResult<IEnumerable<CaballoDTO>>> GetCaballos()
        {
            var caballos = await _caballoService.ObtenerCaballosAsync();

            if (caballos == null || !caballos.Any())
                
                return NotFound("No hay ningun elemento.");

            return Ok(caballos);
        }

        //GET: api/Caballo/5
        [HttpGet("obtenerCaballo/{placa}")]
        public async Task<ActionResult<CaballoDTO>> GetCaballo(string placa)
        {
            var caballo = await _caballoService.ObtenerCaballoAsync(placa);

            if (caballo == null)
            {
                return NotFound("Caballo no encontrado");
            }

            return caballo;
        }

        //// PUT: api/Caballo/5
        [HttpPut("actualizarCaballo/{placa}")]
        public async Task<IActionResult> PutCaballo(string placa, CaballoDTO caballoDTO)
        {
           
            if (placa != caballoDTO.Placa)
            {
                return BadRequest();
            }

            var respuesta = await _caballoService.ActualizarCaballoAsync(placa, caballoDTO);

            if (respuesta) 
            {
                return NotFound("Caballo no encontrado");
            }
            return Ok("Caballo actualizado correctamente");
        }

        //POST: api/Caballo
        [HttpPost("agregarCaballo")]
        public async Task<ActionResult<CaballoDTO>> PostCaballo(CaballoDTO caballoDTO)
        {

            var respuesta = await _caballoService.AgregarCaballoAsync(caballoDTO);
            if (respuesta)
            {
                return  NotFound("Caballo ya existe");
            }

            return Ok("Agreagdo exitosamnete");
        }

        // DELETE: api/Caballo/5
        [HttpDelete("eliminarCaballo{placa}")]
        public async Task<IActionResult> DeleteCaballo(string placa)
        {
            var respuesta = await _caballoService.EliminarCaballoAsync(placa);
            if (respuesta)
            {
                return NotFound("Caballo no encontrado");
            }

            return Ok("Eliminado");
        }
    }
}
