using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface ISesionQueryRepository
    {
        Task<List<SesionDto>> GetAllDtosAsync();
        Task<List<SesionDto>> GetByWeekDtosAsync(DateOnly startOfWeek);
    }
}