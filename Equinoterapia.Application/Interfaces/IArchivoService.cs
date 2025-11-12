using Equinoterapia.Application.DTOs.Archivos;

namespace Equinoterapia.Application.Interfaces
{
    public interface IArchivoService
    {
        // Copia las firmas de tus métodos públicos
        Task<(bool, string)> AddAsync(AddArchivoDto archivoDto, int pacienteId);
        Task<(byte[]?, string, string)> GetAsync(string nombreArchivo, int pacienteId);
        Task<(bool, string)> UpdateAsync(UpdateArchivoDto archivoDto, int pacienteId);
        Task<(bool, string)> DeleteAsync(string nombreArchivo, int pacienteId);
    }
}