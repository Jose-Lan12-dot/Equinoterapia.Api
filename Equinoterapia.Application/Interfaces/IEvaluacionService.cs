using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IEvaluacionService
    {
        Task<(EvaluacionDto?, string?, string)> GetAsync(int pacienteId);
        Task<(bool, string)> AddAsync(EvaluacionDto evaluacionDto, int pacienteId);
        Task<(bool, string)> UpdateAsync(EvaluacionDto evaluacionDto, int pacienteId);
        Task<(bool, string)> DeleteAsync(int pacienteId);
        Task<(IEnumerable<PreguntasDto>, string)> GetPreguntasAsync();
    }
}