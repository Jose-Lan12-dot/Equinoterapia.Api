using Equinoterapia.Application.DTOs.Sesiones;

namespace Equinoterapia.Application.Interfaces
{
    public interface ISesionQueryRepository
    {
        Task<List<GetAllSesionDto>> GetAllDtosAsync();
        Task<List<GetAllSesionDto>> GetByWeekDtosAsync(DateOnly startOfWeek);
    }
}