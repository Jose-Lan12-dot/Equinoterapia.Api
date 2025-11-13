using Equinoterapia.Application.DTOs.Caballos;

namespace Equinoterapia.Application.Interfaces
{
    public interface ICaballoService
    {
        Task<(IEnumerable<GetAllCaballoDto>, string)> GetAllAsync();
        Task<(GetCaballoDto?, string)> GetByIdAsync(int caballoId);
        Task<(bool, string)> AddAsync(AddCaballoDto addCaballoDto);
        Task<(bool, string)> UpdatelAsync(UpdateCaballoDto caballoDto, int caballoId);
        Task<(bool, string)> DeleteAsync(int caballoId);
    }
}