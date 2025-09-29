using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Services
{
    public class pacienteService
    {
        private readonly AppDbContext _context;

        public pacienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PacienteDTO>> ObtenerPacientesAsync()
        {
            return await _context.paciente
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
        }

        public async Task<PacienteDTO?> ObtenerPacienteAsync(int id)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return null;

            return new PacienteDTO
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                FechaNacimiento = paciente.FechaNacimiento,
                Tutor = paciente.Tutor,
                TelefonoTutor = paciente.TelefonoTutor,
                FechaRegistro = paciente.FechaRegistro
            };
        }

        public async Task<bool> ActualizarPacienteAsync(int id, PacienteDTO pacienteDTO)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return false;

            paciente.Nombre = pacienteDTO.Nombre;
            paciente.FechaNacimiento = pacienteDTO.FechaNacimiento;
            paciente.Tutor = pacienteDTO.Tutor;
            paciente.TelefonoTutor = pacienteDTO.TelefonoTutor;
            paciente.FechaRegistro = pacienteDTO.FechaRegistro;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AgregarPacienteAsync(PacienteDTO pacienteDTO)
        {
            if (_context.paciente.Any(p => p.PacienteId == pacienteDTO.PacienteId))
                return false;

            var nuevoPaciente = new Paciente
            {
                Nombre = pacienteDTO.Nombre,
                FechaNacimiento = pacienteDTO.FechaNacimiento,
                Tutor = pacienteDTO.Tutor,
                TelefonoTutor = pacienteDTO.TelefonoTutor,
                FechaRegistro = pacienteDTO.FechaRegistro ?? DateTime.Now
            };

            _context.paciente.Add(nuevoPaciente);
            await _context.SaveChangesAsync();
            pacienteDTO.PacienteId = nuevoPaciente.PacienteId;
            return true;
        }

        public async Task<bool> EliminarPacienteAsync(int id)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return false;

            _context.paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}