using Equinoterapia.Application.DTOs.Caballos;

namespace Equinoterapia.Application.Interfaces
{
    public interface ICaballoService
    {
        Task<(IEnumerable<CaballoDto>, string)> GetAllAsync();
        Task<(CaballoDto?, string)> GetByIdAsync(int caballoId);
        Task<(bool, string)> AddAsync(AddCaballoDto addCaballoDto);
        Task<(bool, string)> UpdatePartialAsync(CaballoDto caballoDto, int caballoId);
        Task<(bool, string)> DeleteAsync(int caballoId);
    }
}