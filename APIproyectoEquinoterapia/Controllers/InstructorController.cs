using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstructorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Instructor
        [HttpGet("obtenerInstructores")]
        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructores()
        {
            var instructores = await _context.instructor
                .Select(i => new InstructorDTO
                {
                    InstructorId = i.InstructorId,
                    Nombre = i.Nombre,
                    Telefono = i.Telefono
                })
                .ToListAsync();

            if (instructores == null || instructores.Count == 0)
                return NotFound("No hay instructores registrados.");

            return instructores;
        }

        // GET: api/Instructor/5
        [HttpGet("obtenerInstructor/{id}")]
        public async Task<ActionResult<InstructorDTO>> GetInstructor(int id)
        {
            var instructor = await _context.instructor
                .Where(i => i.InstructorId == id)
                .Select(i => new InstructorDTO
                {
                    InstructorId = i.InstructorId,
                    Nombre = i.Nombre,
                    Telefono = i.Telefono
                })
                .FirstOrDefaultAsync();

            if (instructor == null)
                return NotFound("Instructor no encontrado.");

            return instructor;
        }

        // POST: api/Instructor
        [HttpPost("agregarInstructor")]
        public async Task<ActionResult<InstructorDTO>> PostInstructor(InstructorDTO instructorDTO)
        {
            
            
            var instructor = new Instructor
            {
                Nombre = instructorDTO.Nombre,
                Telefono = instructorDTO.Telefono
            };

            _context.instructor.Add(instructor);
            await _context.SaveChangesAsync();

            instructorDTO.InstructorId = instructor.InstructorId;

            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.InstructorId }, instructorDTO);
        }

        // PUT: api/Instructor/5
        [HttpPut("actualizarInstructor/{id}")]
        public async Task<IActionResult> PutInstructor(int id, InstructorDTO instructorDTO)
        {
            if (id != instructorDTO.InstructorId)
                return BadRequest("El ID del instructor no coincide.");

            var instructor = await _context.instructor.FindAsync(id);
            if (instructor == null)
                return NotFound("Instructor no encontrado.");

            instructor.Nombre = instructorDTO.Nombre;
            instructor.Telefono = instructorDTO.Telefono;

            await _context.SaveChangesAsync();
            return Ok("Instructor actualizado correctamente.");
        }

        // DELETE: api/Instructor/5
        [HttpDelete("eliminarInstructor/{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _context.instructor.FindAsync(id);
            if (instructor == null)
                return NotFound("Instructor no encontrado.");

            _context.instructor.Remove(instructor);
            await _context.SaveChangesAsync();

            return Ok("Instructor eliminado correctamente.");
        }
    }
}
