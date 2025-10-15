using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Data.Services
{
    public class CitaService
    {
        private readonly AppDbContext _context;

        public CitaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CitaDTO>> GetCitasAsync()
        {
            return await _context.cita
                .Select(c => new CitaDTO
                {
                    SesionId = c.SesionId,
                    PacienteId = c.PacienteId,
                    Asistio = c.Asistio
                })
                .ToListAsync();
        }

        public async Task<CitaDTO?> GetCitaAsync(int id)
        {
            var cita = await _context.cita.FindAsync(id);
            if (cita == null)
                return null;

            return new CitaDTO
            {
                SesionId = cita.SesionId,
                PacienteId = cita.PacienteId,
                Asistio = cita.Asistio
            };
        }

        public async Task<bool> PutCitaAsync(int id, CitaDTO citaDTO)
        {
            var cita = await _context.cita.FindAsync(id);
            if (cita == null)
                return false;

            cita.SesionId = citaDTO.SesionId;
            cita.PacienteId = citaDTO.PacienteId;
            cita.Asistio = citaDTO.Asistio;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostCitaAsync(CitaDTO citaDTO)
        {
            if (!_context.sesion.Any(c => c.SesionId == citaDTO.SesionId) ||
                !_context.paciente.Any(c => c.PacienteId == citaDTO.PacienteId))
            {
                return false;
            }

            var cita = new Cita
            {
                SesionId = citaDTO.SesionId,
                PacienteId = citaDTO.PacienteId,
                Asistio = citaDTO.Asistio
            };

            _context.cita.Add(cita);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCitaAsync(int id)
        {
            var cita = await _context.cita.FindAsync(id);
            if (cita == null)
                return false;

            _context.cita.Remove(cita);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}