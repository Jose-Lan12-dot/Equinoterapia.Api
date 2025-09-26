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
    public class SesionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SesionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sesion
        [HttpGet("obtenerSesiones")]
        public async Task<ActionResult<IEnumerable<SesionDTO>>> GetSesiones()
        {
            var sesiones = await _context.sesion
                .Select(s => new SesionDTO
                {
                    SesionId = s.SesionId,
                    InstructorId = s.InstructorId,
                    Placa = s.Placa,
                    Fecha = s.Fecha,
                    Hora = s.Hora
                })
                .ToListAsync();

            if (sesiones == null || sesiones.Count == 0)
                return NotFound("No hay sesiones registradas.");

            return sesiones;
        }

        // GET: api/Sesion/5
        [HttpGet("obtenerSesion/{id}")]
        public async Task<ActionResult<SesionDTO>> GetSesion(int id)
        {
            var sesion = await _context.sesion
                .Where(s => s.SesionId == id)
                .Select(s => new SesionDTO
                {
                    SesionId = s.SesionId,
                    InstructorId = s.InstructorId,
                    Placa = s.Placa,
                    Fecha = s.Fecha,
                    Hora = s.Hora
                })
                .FirstOrDefaultAsync();

            if (sesion == null)
                return NotFound("Sesión no encontrada.");

            return sesion;
        }

        // POST: api/Sesion
        [HttpPost("agregarSesion")]
        public async Task<ActionResult<SesionDTO>> PostSesion(SesionDTO sesionDTO)
        {

            if (!_context.instructor.Any(c => c.InstructorId == sesionDTO.InstructorId) || 
                !_context.caballo.Any(c => c.Placa == sesionDTO.Placa)) {
                return NotFound("Caballo o instrucctor no existe");
            }
            
            // No uses sesionDTO.SesionId aquí, la base de datos lo genera
            var sesion = new Sesion
            {
                InstructorId = sesionDTO.InstructorId,
                Placa = sesionDTO.Placa,
                Fecha = sesionDTO.Fecha,
                Hora = sesionDTO.Hora
            };

            _context.sesion.Add(sesion);
            await _context.SaveChangesAsync();

            // Ahora sí, asigna el SesionId generado a tu DTO para devolverlo al frontend
            sesionDTO.SesionId = sesion.SesionId;

            return Ok("Agregado Exitosamnete");
        }

        // PUT: api/Sesion/5
        [HttpPut("actualizarSesion/{id}")]
        public async Task<IActionResult> PutSesion(int id, SesionDTO sesionDTO)
        {
            if (id != sesionDTO.SesionId)
                return BadRequest("El ID de la sesión no coincide.");

            var sesion = await _context.sesion.FindAsync(id);
            if (sesion == null)
                return NotFound("Sesión no encontrada.");

            sesion.InstructorId = sesionDTO.InstructorId;
            sesion.Placa = sesionDTO.Placa;
            sesion.Fecha = sesionDTO.Fecha;
            sesion.Hora = sesionDTO.Hora;

            await _context.SaveChangesAsync();
            return Ok("Sesión actualizada correctamente.");
        }

        // DELETE: api/Sesion/5
        [HttpDelete("eliminarSesion/{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _context.sesion.FindAsync(id);
            if (sesion == null)
                return NotFound("Sesión no encontrada.");

            _context.sesion.Remove(sesion);
            await _context.SaveChangesAsync();

            return Ok("Sesión eliminada correctamente.");
        }
    }
}
