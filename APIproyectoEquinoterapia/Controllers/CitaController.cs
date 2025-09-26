    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIproyectoEquinoterapia.Models;
using APIproyectoEquinoterapia.DTOs;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cita
        [HttpGet("obtenerCitas")]
        public async Task<ActionResult<IEnumerable<CitaDTO>>> GetCita()
        {
            var citas = await _context.cita
                .Select(c => new CitaDTO
                {
                    SesionId = c.SesionId,
                    PacienteId = c.PacienteId,
                    Asistio = c.Asistio
                })
                .ToListAsync();

            if (citas == null || citas.Count == 0)
            {
                return NotFound("No se encontraron citas.");
            }

            return citas;
        }

        // GET: api/Cita/5
        [HttpGet("obtenerCita{id}")]
        public async Task<ActionResult<CitaDTO>> GetCita(int id)
        {
            var cita = await _context.cita
                .Where(c => c.CitaId == id)
                .Select(c => new CitaDTO
                {
                    SesionId = c.SesionId,
                    PacienteId = c.PacienteId,
                    Asistio = c.Asistio
                })
                .FirstOrDefaultAsync();

            if (cita == null)
            {
                return NotFound("Cita no encontrada");
            }

            return cita;
        }

        // PUT: api/Cita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CitaDTO citaDTO)
        {
            var cita = await _context.cita.FindAsync(id);
            if (cita == null)
                return NotFound();

            cita.SesionId = citaDTO.SesionId;
            cita.PacienteId = citaDTO.PacienteId;
            cita.Asistio = citaDTO.Asistio;

            await _context.SaveChangesAsync();
            return Ok("Cita actualizada correctamente.");
        }

        // POST: api/Cita
        [HttpPost]
        public async Task<ActionResult<CitaDTO>> PostCita(CitaDTO citaDTO)
        {

            if (!_context.sesion.Any(c => c.SesionId == citaDTO.SesionId) ||
                !_context.paciente.Any(c => c.PacienteId == citaDTO.PacienteId))
            {
                return NotFound("Sesion o Paciente no existe");
            }
            var cita = new Cita
            {
                SesionId = citaDTO.SesionId,
                PacienteId = citaDTO.PacienteId,
                Asistio = citaDTO.Asistio
            };

            _context.cita.Add(cita);
            await _context.SaveChangesAsync();

            citaDTO.SesionId = cita.SesionId;
            citaDTO.PacienteId = cita.PacienteId;
            citaDTO.Asistio = cita.Asistio;

            return CreatedAtAction("GetCita", new { id = cita.CitaId }, citaDTO);
        }

        // DELETE: api/Cita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.cita.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            _context.cita.Remove(cita);
            await _context.SaveChangesAsync();

            return Ok("Cita eliminada correctamente.");
        }

    }
}
