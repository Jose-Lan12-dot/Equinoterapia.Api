using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [Authorize]
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


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInstructorDto instructorDto)
        {


            var (exito, mensaje) = await _instructorService.UpdateAsync(instructorDto, id);

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
