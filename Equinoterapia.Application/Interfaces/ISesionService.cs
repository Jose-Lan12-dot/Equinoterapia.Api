using Equinoterapia.Application.DTOs.Sesiones;
using Equinoterapia.Application.DTOs.Shared;

namespace Equinoterapia.Application.Interfaces
{
    public interface ISesionService
    {
        Task<(IEnumerable<GetAllSesionDto>, string)> GetAllAsync();
        Task<(GetOneSesionDto?, string)> GetByIdAsync(int sesionId);
        Task<(bool, string)> AddAsync(AddSesionDto addSesionDto);
        Task<(bool, string)> UpdateAsync(UpdateSesionDto sesionDto, int sesionId);
        Task<(bool, string)> DeleteAsync(int sesionId);
        Task<(IEnumerable<GetAllSesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek);
        Task<SesionFormDataDto> GetFormDataAsync();
        Task<List<HorarioOcupadoDto>> GetHorariosOcupadosAsync(DateOnly fecha);
    }
}