using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class AsistenciaRepository : IAsistenciaRepository, IAsistenciaQueryRepository
    {
        private readonly AppDbContext _context;

        public AsistenciaRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<AsistenciaDto>> GetAsistenciasBySesionIdAsync(int sesionId)
        {

            return await _context.asistencia
                .Where(a => a.SesionId == sesionId)
                .Join(_context.paciente,
                      asistencia => asistencia.PacienteId, 
                      paciente => paciente.PacienteId,    
                      (asistencia, paciente) => new AsistenciaDto 
                      {
                          AsistenciaId = asistencia.AsistenciaId,
                          nombre = paciente.Nombre, 
                          Asistio = asistencia.Asistio
                      })
                .ToListAsync();
        }


        public async Task<bool> SesionExistsAsync(int sesionId)
        {
            return await _context.sesion.AnyAsync(s => s.SesionId == sesionId);
        }

        public async Task<List<int>> GetExistingPacienteIdsAsync(List<int> pacienteIds)
        {
            return await _context.paciente
                .Where(p => pacienteIds.Contains(p.PacienteId))
                .Select(p => p.PacienteId)
                .ToListAsync();
        }

        public async Task<List<Asistencia>> GetAsistenciasByIdsAsync(List<int> asistenciaIds)
        {
            return await _context.asistencia
                .Where(a => asistenciaIds.Contains(a.AsistenciaId))
                .ToListAsync();
        }

        public void AddRange(IEnumerable<Asistencia> asistencias)
        {
            _context.asistencia.AddRange(asistencias);
        }

        public void RemoveRange(IEnumerable<Asistencia> asistencias)
        {
            _context.asistencia.RemoveRange(asistencias);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Asistencia>> GetByPacienteIdAsync(int pacienteId)
        {
            return await _context.asistencia
                .Where(a => a.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<List<Asistencia>> GetBySesionIdsAsync(List<int> sesionIds)
        {
            return await _context.asistencia
                .Where(a => sesionIds.Contains(a.SesionId))
                .ToListAsync();
        }
    }
}