using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IAsistenciaRepository
    {
        // Métodos de Lectura
        Task<bool> SesionExistsAsync(int sesionId);
        Task<List<int>> GetExistingPacienteIdsAsync(List<int> pacienteIds);
        Task<List<Asistencia>> GetAsistenciasByIdsAsync(List<int> asistenciaIds);
        Task<List<Asistencia>> GetBySesionIdsAsync(List<int> sesionIds);
        Task<List<Asistencia>> GetByPacienteIdAsync(int pacienteId);

        // Métodos de Escritura
        void AddRange(IEnumerable<Asistencia> asistencias);
        void RemoveRange(IEnumerable<Asistencia> asistencias);
        Task<bool> SaveChangesAsync();

    }
}