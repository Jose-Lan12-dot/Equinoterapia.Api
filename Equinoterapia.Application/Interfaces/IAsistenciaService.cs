using Equinoterapia.Application.DTOs; 

namespace Equinoterapia.Application.Interfaces
{
    public interface IAsistenciaService
    {
        Task<(IEnumerable<AsistenciaDto>, string)> GetAsync(int sesionId);

        Task<(bool, string)> AddAsync(AsistenciaAddDto asistenciaAdd);

        Task<(string, bool)> UpdateAsync(AsistenciaUpdateDto asistenciaUpdate);
        Task<(string, bool)> DeleteAsync(List<int> asistenciaId);
    }
}