using Equinoterapia.Application.DTOs.Evaluaciones;

namespace Equinoterapia.Application.Interfaces
{
    public interface IEvaluacionService
    {
        Task<(List<GetAllEvaluacionesAsyncDto>, string)> GetAllAsync(int pacienteId);
        Task<(GetEvaluacionDto?, string)> GetAsync(int evaluacionId);
        Task<(bool, string)> AddAsync(AddEvaluacionDto evaluacionDto, int pacienteId);
        Task<(bool, string)> UpdateAsync(UpdateEvaluacionDto evaluacionDto, int evaluacionId);
        Task<(bool, string)> DeleteAsync(int evaluacionId);
        Task<(List<PreguntasDto>, string)> GetPreguntasAsync();
    }
}