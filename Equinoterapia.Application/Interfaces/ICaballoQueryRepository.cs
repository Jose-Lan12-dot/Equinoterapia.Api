using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface ICaballoQueryRepository
    {
        Task<List<CaballoDto>> GetAllDtosAsync();
        Task<List<DropdownItemDto>> GetDisponiblesAsync();
    }
}