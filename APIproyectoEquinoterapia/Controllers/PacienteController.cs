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
    public class PacienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Paciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacientes()
        {
            var pacientes = await _context.paciente
                .Select(p => new PacienteDTO
                {
                    PacienteId = p.PacienteId,
                    Nombre = p.Nombre,
                    FechaNacimiento = p.FechaNacimiento,
                    Tutor = p.Tutor,
                    TelefonoTutor = p.TelefonoTutor,
                    FechaRegistro = p.FechaRegistro
                })
                .ToListAsync();

            if (pacientes == null || pacientes.Count == 0)
                return NotFound("No hay pacientes registrados.");

            return pacientes;
        }

        // GET: api/Paciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(int id)
        {
            var paciente = await _context.paciente
                .Where(p => p.PacienteId == id)
                .Select(p => new PacienteDTO
                {
                    PacienteId = p.PacienteId,
                    Nombre = p.Nombre,
                    FechaNacimiento = p.FechaNacimiento,
                    Tutor = p.Tutor,
                    TelefonoTutor = p.TelefonoTutor,
                    FechaRegistro = p.FechaRegistro
                })
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            return paciente;
        }

        // PUT: api/Paciente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteDTO pacienteDTO)
        {
            if (id != pacienteDTO.PacienteId)
            {
                return BadRequest();
            }

            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return NotFound("Paciente no encontrado.");

            paciente.Nombre = pacienteDTO.Nombre;
            paciente.FechaNacimiento = pacienteDTO.FechaNacimiento;
            paciente.Tutor = pacienteDTO.Tutor;
            paciente.TelefonoTutor = pacienteDTO.TelefonoTutor;
            paciente.FechaRegistro = pacienteDTO.FechaRegistro;

            await _context.SaveChangesAsync();
            return Ok("Paciente actualizado correctamente.");
        }

        // POST: api/Paciente
        [HttpPost]
        public async Task<ActionResult<PacienteDTO>> PostPaciente(PacienteDTO pacienteDTO)
        {
            var paciente = new Paciente
            {
                Nombre = pacienteDTO.Nombre,
                FechaNacimiento = pacienteDTO.FechaNacimiento,
                Tutor = pacienteDTO.Tutor,
                TelefonoTutor = pacienteDTO.TelefonoTutor,
                FechaRegistro = pacienteDTO.FechaRegistro
            };

            _context.paciente.Add(paciente);
            await _context.SaveChangesAsync();

            pacienteDTO.PacienteId = paciente.PacienteId;

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.PacienteId }, pacienteDTO);
        }

        // DELETE: api/Paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            _context.paciente.Remove(paciente);
            await _context.SaveChangesAsync();

            return Ok("Paciente eliminado correctamente.");
        }

        private bool PacienteExists(int id)
        {
            return _context.paciente.Any(e => e.PacienteId == id);
        }
    }
}
