using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    [Authorize]
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
            var (instructores, mensaje) = await _instructorService.GetInstructoresAsync();
            if (instructores == null || !instructores.Any())
                return NotFound(mensaje);
            return Ok(instructores);
        }

        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<InstructorDTO>> GetInstructor(int id)
        {
            var (instructor, mensaje) = await _instructorService.GetInstructorAsync(id);
            if (instructor == null)
                return NotFound(mensaje);
            return Ok(instructor);
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> PostInstructor(InstructorDTO instructorDTO)
        {
            var (respuesta, mensaje) = await _instructorService.PostInstructorAsync(instructorDTO);
            if (!respuesta)
                return Conflict(mensaje);
            return Ok(mensaje);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutInstructor(int id, InstructorDTO instructorDTO)
        {
            var (respuesta, mensaje) = await _instructorService.PutInstructorAsync(id, instructorDTO);
            if (!respuesta)
                return NotFound(mensaje);
            return Ok(mensaje);
        }
    }
}
