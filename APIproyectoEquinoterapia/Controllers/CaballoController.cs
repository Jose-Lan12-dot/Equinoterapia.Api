using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIproyectoEquinoterapia.Models;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caballo>>> GetCaballos()
        {
            return await _context.Caballos.ToListAsync();
        }

        // GET: api/Caballo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caballo>> GetCaballo(string id)
        {
            var caballo = await _context.Caballos.FindAsync(id);

            if (caballo == null)
            {
                return NotFound();
            }

            return caballo;
        }

        // PUT: api/Caballo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaballo(string id, Caballo caballo)
        {
            if (id != caballo.Placa)
            {
                return BadRequest();
            }

            _context.Entry(caballo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaballoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Caballo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Caballo>> PostCaballo(Caballo caballo)
        {
            _context.Caballos.Add(caballo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CaballoExists(caballo.Placa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCaballo", new { id = caballo.Placa }, caballo);
        }

        // DELETE: api/Caballo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaballo(string id)
        {
            var caballo = await _context.Caballos.FindAsync(id);
            if (caballo == null)
            {
                return NotFound();
            }

            _context.Caballos.Remove(caballo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaballoExists(string id)
        {
            return _context.Caballos.Any(e => e.Placa == id);
        }
    }
}
