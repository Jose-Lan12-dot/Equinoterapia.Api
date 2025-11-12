using Equinoterapia.Application.DTOs.Caballos;
using Equinoterapia.Application.DTOs.Shared;

namespace Equinoterapia.Application.Interfaces
{
    public interface ICaballoQueryRepository
    {
        Task<List<CaballoDto>> GetAllDtosAsync();
        Task<List<DropdownItemDto>> GetDisponiblesAsync();
    }
}