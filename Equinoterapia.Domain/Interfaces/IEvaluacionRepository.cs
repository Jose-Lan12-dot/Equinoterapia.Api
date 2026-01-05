using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IEvaluacionRepository
    {
        // --- Métodos de Lectura ---
        Task<bool> PacienteExistsAsync(int pacienteId);
        Task<Evaluacion?> GetEvaluacionByIdAsync(int EvaluacionId);
        Task<List<Evaluacion>> GetEvaluacionesByPacienteId(int pacienteId);
        Task<string?> GetPacienteNombreAsync(int pacienteId);
        Task<int> GetPreguntasCountAsync();
        Task<bool> EvaluacionExistsAsync(int pacienteId);


        // --- Métodos de Escritura ---
        void Add(Evaluacion evaluacion);
        void Delete(Evaluacion evaluacion);
        void RemoveRange(List<Evaluacion> evaluacions);
        Task<bool> SaveChangesAsync();

    }
}