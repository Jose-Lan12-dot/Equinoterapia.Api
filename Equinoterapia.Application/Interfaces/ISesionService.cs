using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface ISesionService
    {
        Task<(IEnumerable<SesionDto>, string)> GetAllAsync();
        Task<(SesionDto?, string)> GetByIdAsync(int sesionId);
        Task<(bool, string)> AddAsync(AddSesionDto addSesionDto);
        Task<(bool, string)> UpdateAsync(SesionDto sesionDto, int sesionId);
        Task<(bool, string)> DeleteAsync(int sesionId);
        Task<(IEnumerable<SesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek);
        Task<SesionFormDataDto> GetFormDataAsync();
        Task<List<HorarioOcupadoDto>> GetHorariosOcupadosAsync(DateOnly fecha);
    }
}