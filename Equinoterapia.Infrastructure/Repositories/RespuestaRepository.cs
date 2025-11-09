using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class RespuestaRepository : IRespuestaRepository
    {
        private readonly AppDbContext _context;

        public RespuestaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Respuestas>> GetRespuestasByEvaluacionIdAsync(int evaluacionId)
        {
            return await _context.respuestas
                .Where(r => r.EvaluacionId == evaluacionId)
                .OrderBy(r => r.PreguntaId)
                .ToListAsync();
        }

        public void AddRange(IEnumerable<Respuestas> respuestas)
        {
            _context.respuestas.AddRange(respuestas);
        }

        public void RemoveRange(IEnumerable<Respuestas> respuestas)
        {
            _context.respuestas.RemoveRange(respuestas);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}