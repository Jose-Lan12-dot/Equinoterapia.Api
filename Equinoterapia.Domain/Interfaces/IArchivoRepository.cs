using Equinoterapia.Domain.Entities; // Asumo que tus entidades se llaman Archivos y TipoArchivo

namespace Equinoterapia.Domain.Interfaces
{
    public interface IArchivoRepository
    {
        // --- Métodos de Lectura ---
        Task<TipoArchivo?> GetTipoArchivoPorNombreAsync(string nombreArchivo);
        Task<Archivos?> GetArchivoAsync(int pacienteId, int tipoId);
        Task<bool> PacienteExistsAsync(int pacienteId);
        Task<bool> ArchivoExistsAsync(int tipoId, int pacienteId);
        Task<int> GetTipoIdPorNombreAsync(string nombreArchivo);

        // --- Métodos de Escritura ---
        void Add(Archivos archivo);
        void Delete(Archivos archivo);
        Task<bool> SaveChangesAsync();


        Task<List<Archivos>> GetByPacienteIdAsync(int pacienteId);
        void RemoveRange(IEnumerable<Archivos> archivos);
    }
}