using Equinoterapia.Application.DTOs.Evaluaciones;

namespace Equinoterapia.Application.Interfaces
{
    public interface IEvaluacionQueryRepository
    {
        Task<List<PreguntasDto>> GetPreguntasDtoAsync();
        Task<List<GetAllEvaluacionesAsyncDto>> GetEvaluacionesByPacienteIdAsync(int pacienteId);
    }
}