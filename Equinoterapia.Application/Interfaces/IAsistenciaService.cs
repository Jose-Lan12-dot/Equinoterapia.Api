using Equinoterapia.Application.DTOs.Asistencias;

namespace Equinoterapia.Application.Interfaces
{
    public interface IAsistenciaService
    {
        Task<(IEnumerable<GetAsistenciaDto>, string)> GetAsync(int sesionId);

        Task<(bool, string)> AddAsync(AddAsistenciaDto asistenciaAdd);

        Task<(string, bool)> UpdateAsync(UpdateAsistenciaDto asistenciaUpdate);
        Task<(string, bool)> DeleteAsync(List<int> asistenciaId);
    }
}