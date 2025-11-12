using Equinoterapia.Application.DTOs.Evaluaciones;

namespace Equinoterapia.Application.Interfaces
{
    public interface IEvaluacionService
    {
        Task<(GetEvaluacionDto?, string)> GetAsync(int pacienteId);
        Task<(bool, string)> AddAsync(AddEvaluacionDto evaluacionDto, int pacienteId);
        Task<(bool, string)> UpdateAsync(UpdateEvaluacionDto evaluacionDto, int pacienteId);
        Task<(bool, string)> DeleteAsync(int pacienteId);
        Task<(List<PreguntasDto>, string)> GetPreguntasAsync();
    }
}