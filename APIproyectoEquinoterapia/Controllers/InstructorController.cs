using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        
        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructores()
        {
            var instructores = await _instructorService.GetInstructoresAsync();
            if (instructores == null || !instructores.Any())
                return NotFound("No hay instructores registrados.");
            return Ok(instructores);
        }

        
        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<InstructorDTO>> GetInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorAsync(id);
            if (instructor == null)
                return NotFound("Instructor no encontrado.");
            return Ok(instructor);
        }

        
        [HttpPost("agregar")]
        public async Task<ActionResult<InstructorDTO>> PostInstructor(InstructorDTO instructorDTO)
        {
            await _instructorService.PostInstructorAsync(instructorDTO);
            return Ok(instructorDTO);
        }

        
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutInstructor(int id, InstructorDTO instructorDTO)
        {
            if (id != instructorDTO.InstructorId)
                return BadRequest("El ID del instructor no coincide.");

            var actualizado = await _instructorService.PutInstructorAsync(id, instructorDTO);
            if (!actualizado)
                return NotFound("Instructor no encontrado.");

            return Ok("Instructor actualizado correctamente.");
        }

        
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var eliminado = await _instructorService.DeleteInstructorAsync(id);
            if (!eliminado)
                return NotFound("Instructor no encontrado.");

            return Ok("Instructor eliminado correctamente.");
        }
    }
}
