using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructoresController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructoresController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (instructores, mensaje) = await _instructorService.GetAllAsync();

            if (!instructores.Any())
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return Ok(instructores); 
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (instructor, mensaje) = await _instructorService.GetByIdAsync(id);

            if (instructor == null)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return Ok(instructor); 
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddInstructorDto addInstructorDto)
        {
            var (exito, mensaje) = await _instructorService.AddAsync(addInstructorDto);

            if (!exito)
            {
                return BadRequest(new { Mensaje = mensaje }); 
            }

            return Ok(new { Mensaje = mensaje });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAll(int id, [FromBody] InstructorDto instructorDto)
        {
            if (id != instructorDto.InstructorId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _instructorService.UpdateAllAsync(instructorDto, id);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] InstructorDto instructorDto)
        {
            if (id != instructorDto.InstructorId)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var (exito, mensaje) = await _instructorService.UpdatePartialAsync(instructorDto, id);

            if (!exito)
                return NotFound(new { Mensaje = mensaje });

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (exito, mensaje) = await _instructorService.DeleteAsync(id);

            if (!exito)
            {
                return NotFound(new { Mensaje = mensaje }); 
            }

            return NoContent(); 
        }


    }


}
