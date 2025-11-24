using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        // Métodos que devuelven datos
        Task<Paciente?> GetByIdAsync(int pacienteId);
        Task<int> GetFaltasCountAsync(int pacienteId);
        Task<bool> HasEvaluacionAsync(int pacienteId);
        Task<List<string>> GetMissingFileTypesAsync(int pacienteId);
        Task<int> GetTotalFileTypesCountAsync();
        Task<int> GetArchivosCountAsync(int pacienteId);
        Task<Enum> PacienteEstado(int pacienteId);

        // Métodos que modifican datos (void)
        void Add(Paciente paciente);
        void Delete(Paciente paciente);

        Task<bool> SaveChangesAsync();


    }
}
