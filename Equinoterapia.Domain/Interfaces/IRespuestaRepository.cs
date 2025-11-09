using Equinoterapia.Domain.Entities; // Asumo que tu entidad se llama Respuestas

namespace Equinoterapia.Domain.Interfaces
{
    public interface IRespuestaRepository
    {
        // Métodos de Lectura
        Task<List<Respuestas>> GetRespuestasByEvaluacionIdAsync(int evaluacionId);

        // Métodos de Escritura
        void AddRange(IEnumerable<Respuestas> respuestas);
        void RemoveRange(IEnumerable<Respuestas> respuestas);
        Task<bool> SaveChangesAsync();
    }
}