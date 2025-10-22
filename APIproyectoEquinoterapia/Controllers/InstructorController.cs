using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

//////////////////////
        [HttpGet("obtener-todos")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetAll()
        {
            var (instructores, mensaje) = await _instructorService.GetAllAsync();
            if (instructores.Count() == 0)
                return NotFound(mensaje);
            return Ok(new { mensaje, instructores });
        }

//////////////////////
        [HttpGet("obtener-por-id/{instructorId}")]
        public async Task<ActionResult<InstructorDto>> GetById(int instructorId)
        {
            var (instructor, mensaje) = await _instructorService.GetByIdAsync(instructorId);
            if (instructor == null)
                return NotFound(mensaje);
            return Ok(new { mensaje, instructor });
        }

//////////////////////
        [HttpPost("agregar")]
        public async Task<IActionResult> Add(AddInstructorDto addInstructorDto)
        {
            var (exito, mensaje) = await _instructorService.AddAsync(addInstructorDto);
            if (!exito)
                return Conflict(mensaje);
            return Ok(mensaje);
        }

//////////////////////
        [HttpPut("actualizar-todo/{instructorId}")]
        public async Task<IActionResult> UpdateAll(InstructorDto instructorDto, int instructorId)
        {
            var (exito, mensaje) = await _instructorService.UpdateAllAsync(instructorDto, instructorId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }
//////////////////////
        [HttpPatch("actualizar-parcialmente/{instructorId}")]
        public async Task<IActionResult> UpdatePartial(InstructorDto instructorDto, int instructorId)
        {
            var (exito, mensaje) = await _instructorService.UpdatePartialAsync(instructorDto, instructorId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }

//////////////////////
        [HttpDelete("eliminar/{instructorId}")]
        public async Task<IActionResult> Delete(int instructorId)
        {
            var (exito, mensaje) = await _instructorService.DeleteAsync(instructorId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }
    }
}
