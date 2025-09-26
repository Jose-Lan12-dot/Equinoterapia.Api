using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaballoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CaballoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Caballo
        [HttpGet("obtenerCaballos")]
        public async Task<ActionResult<IEnumerable<CaballoDTO>>> GetCaballos()
        {
            var caballos = await _context.caballo
                .Select(c => new CaballoDTO
                {
                    Placa = c.Placa,
                    Nombre = c.Nombre,
                    Estado = c.Estado,
                    FechaHerraje = c.FechaHerraje,
                    Padecimiento = c.Padecimiento,
                })
                .ToListAsync();

            if (caballos == null || caballos.Count == 0)
                return NotFound("No hay ningun elemento.");

            return caballos;
        }

        //GET: api/Caballo/5
        [HttpGet("obtenerCaballo/{placa}")]
        public async Task<ActionResult<CaballoDTO>> GetCaballo(string placa)
        {
            var caballo = await _context.caballo
                .Where(c => c.Placa == placa)
                .Select(c => new CaballoDTO
                {
                    Placa = c.Placa,
                    Nombre = c.Nombre,
                    Estado = c.Estado,
                    FechaHerraje = c.FechaHerraje,
                    Padecimiento = c.Padecimiento,
                })
                .FirstOrDefaultAsync();

            if (caballo == null)
            {
                return NotFound("Caballo no encontrado");
            }

            return caballo;
        }

        //// PUT: api/Caballo/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("actualizarCaballo/{placa}")]
        public async Task<IActionResult> PutCaballo(string placa, CaballoDTO caballoDTO)
        {
            if (placa != caballoDTO.Placa)
            {
                return BadRequest();
            }

            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
                return NotFound("Caballo no encontrado");

            // Actualiza los campos
            caballo.Nombre = caballoDTO.Nombre;
            caballo.Estado = caballoDTO.Estado;
            caballo.FechaHerraje = caballoDTO.FechaHerraje;
            caballo.Padecimiento = caballoDTO.Padecimiento;

            await _context.SaveChangesAsync();
            return Ok("Caballo actualizado correctamente");
        }

        //POST: api/Caballo
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("agregarCaballo")]
        public async Task<ActionResult<CaballoDTO>> PostCaballo(CaballoDTO caballoDTO)
        {


            if (_context.caballo.Any(c => c.Placa == caballoDTO.Placa))
            {
                return  NotFound("Caballo no encontrado");
            }
            else {
                var caballo = new Caballo
                {
                    Placa = caballoDTO.Placa,
                    Nombre = caballoDTO.Nombre,
                    Estado = caballoDTO.Estado,
                    FechaHerraje = caballoDTO.FechaHerraje,
                    Padecimiento = caballoDTO.Padecimiento
                };
                _context.caballo.Add(caballo);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                    throw;
                }
            }

 



            return Ok();
        }

        // DELETE: api/Caballo/5
        [HttpDelete("eliminarCaballo{placa}")]
        public async Task<IActionResult> DeleteCaballo(string placa)
        {
            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
            {
                return NotFound("Caballo no encontrado");
            }

            _context.caballo.Remove(caballo);
            await _context.SaveChangesAsync();

            return Ok("Eliminado");
        }
    }
}
