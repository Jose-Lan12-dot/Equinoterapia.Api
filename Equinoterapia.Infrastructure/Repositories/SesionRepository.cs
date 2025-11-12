using Equinoterapia.Application.DTOs.Sesiones;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class SesionRepository : ISesionRepository, ISesionQueryRepository
    {
        private readonly AppDbContext _context;

        public SesionRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Sesion?> GetByIdAsync(int sesionId)
        {
            return await _context.sesion.FindAsync(sesionId);
        }

        public void Add(Sesion sesion)
        {
            _context.sesion.Add(sesion);
        }

        public void Delete(Sesion sesion)
        {
            _context.sesion.Remove(sesion);
        }

        public async Task<List<Asistencia>> GetAsistenciasBySesionIdAsync(int sesionId)
        {
            return await _context.asistencia
               .Where(a => a.SesionId == sesionId)
               .ToListAsync();
        }

        public void RemoveAsistencias(IEnumerable<Asistencia> asistencias)
        {
            _context.asistencia.RemoveRange(asistencias);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<List<SesionDto>> GetAllDtosAsync()
        {
            return await _context.sesion
                .AsNoTracking()
                .Select(s => new SesionDto
                {
                    SesionId = s.SesionId,
                    Fecha = s.Fecha,
                    Hora = s.Hora,
                    InstructorId = s.InstructorId,
                    CaballoId = s.CaballoId
                })
                .ToListAsync();
        }

        public async Task<List<SesionDto>> GetByWeekDtosAsync(DateOnly startOfWeek)
        {
            var endOfWeek = startOfWeek.AddDays(6);
            return await _context.sesion
                .AsNoTracking()
                .Where(s => s.Fecha >= startOfWeek && s.Fecha <= endOfWeek)
                .Select(s => new SesionDto
                {
                    SesionId = s.SesionId,
                    Fecha = s.Fecha,
                    Hora = s.Hora,
                    InstructorId = s.InstructorId,
                    CaballoId = s.CaballoId
                })
                .ToListAsync();
        }


        public async Task<List<Sesion>> GetByCaballoIdAsync(int caballoId)
        {
            return await _context.sesion
                .Where(s => s.CaballoId == caballoId)
                .ToListAsync();
        }

        public async Task<List<Sesion>> GetByInstructorIdAsync(int instructorId)
        {
            return await _context.sesion
                .Where(s => s.InstructorId == instructorId)
                .ToListAsync();
        }

        public void RemoveRange(IEnumerable<Sesion> sesiones)
        {
            _context.sesion.RemoveRange(sesiones);
        }

        public async Task<List<Sesion>> GetByFechaAsync(DateOnly fecha)
        {
            return await _context.sesion
                .Where(s => s.Fecha == fecha)
                .ToListAsync();
        }
    }
}