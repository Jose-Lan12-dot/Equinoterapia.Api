using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IArchivoService
    {
        // Copia las firmas de tus métodos públicos
        Task<(bool, string)> AddAsync(ArchivoDto archivos, int pacienteId);
        Task<(byte[]?, string, string)> GetAsync(string nombreArchivo, int pacienteId);
        Task<(bool, string)> UpdateAsync(ArchivoDto archivoDto, int pacienteId);
        Task<(bool, string)> DeleteAsync(string nombreArchivo, int pacienteId);
    }
}