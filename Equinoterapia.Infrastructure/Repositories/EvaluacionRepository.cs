using Equinoterapia.Application.DTOs.Evaluaciones;
using Equinoterapia.Application.DTOs.Evaluaciones;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class EvaluacionRepository : IEvaluacionRepository, IEvaluacionQueryRepository
    {
        private readonly AppDbContext _context;

        public EvaluacionRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> PacienteExistsAsync(int pacienteId)
        {
            return await _context.paciente.AnyAsync(p => p.PacienteId == pacienteId);
        }

        public async Task<Evaluacion?> GetEvaluacionByIdAsync(int evaluacionId)
        {
            return await _context.evaluacion.FindAsync(evaluacionId);
        }

        public async Task<List<GetAllEvaluacionesAsyncDto>> GetEvaluacionesByPacienteIdAsync(int pacienteId)
        {
            return await _context.evaluacion
                .Where(p => p.PacienteId == pacienteId)
                .AsNoTracking()
                .Select(e => new GetAllEvaluacionesAsyncDto
                {
                    EvaluacionId = e.EvaluacionId,
                    Fecha = e.Fecha,
                })
                .ToListAsync();
        }

        public async Task<List<Evaluacion>> GetEvaluacionesByPacienteId(int pacienteId)
        {
            return await _context.evaluacion
                .Where(p => p.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<string?> GetPacienteNombreAsync(int pacienteId)
        {
            return await _context.paciente
                .Where(p => p.PacienteId == pacienteId)
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PreguntasDto>> GetPreguntasDtoAsync()
        {
            return await _context.preguntas
                .OrderBy(p => p.PreguntaId)
                .Select(p => new PreguntasDto
                {
                    Orden = p.PreguntaId,
                    Pregunta = p.Pregunta,
                })
                .ToListAsync();
        }

        public async Task<int> GetPreguntasCountAsync()
        {
            return await _context.preguntas.CountAsync();
        }

        public async Task<bool> EvaluacionExistsAsync(int pacienteId)
        {
            return await _context.evaluacion.AnyAsync(e => e.PacienteId == pacienteId);
        }

        // --- Escritura ---
        public void Add(Evaluacion evaluacion)
        {
            _context.evaluacion.Add(evaluacion);
        }

        public void Delete(Evaluacion evaluacion)
        {
            _context.evaluacion.Remove(evaluacion);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void RemoveRange(List<Evaluacion> evaluacions)
        {
            _context.evaluacion.RemoveRange(evaluacions);
        }
    }
}