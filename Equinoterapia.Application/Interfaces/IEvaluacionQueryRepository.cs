using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IEvaluacionQueryRepository
    {
        Task<IEnumerable<PreguntasDto>> GetPreguntasDtoAsync();
    }
}