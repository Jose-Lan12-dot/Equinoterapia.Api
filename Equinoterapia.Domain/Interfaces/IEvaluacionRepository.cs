using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    // OJO: Este repositorio tendrá que saber del PreguntasDto
    // Esto es un 'atajo' que tomamos, similar a IPacienteQueryRepository
    // Lo ideal sería que devolviera la entidad Pregunta.

    public interface IEvaluacionRepository
    {
        // --- Métodos de Lectura ---
        Task<bool> PacienteExistsAsync(int pacienteId);
        Task<Evaluacion?> GetEvaluacionByPacienteIdAsync(int pacienteId);
        Task<string?> GetPacienteNombreAsync(int pacienteId);
        Task<int> GetPreguntasCountAsync();
        Task<bool> EvaluacionExistsAsync(int pacienteId);

        // --- Métodos de Escritura ---
        void Add(Evaluacion evaluacion);
        void Delete(Evaluacion evaluacion);
        Task<bool> SaveChangesAsync();
    }
}