using Equinoterapia.Application.DTOs.Evaluaciones;

namespace Equinoterapia.Application.Interfaces
{
    public interface IRespuestasService
    {
        Task<(List<RespuestasDto>, string)> Get(int evaluacionId);
        Task<(bool, string)> Add(IEnumerable<RespuestasDto> respuestas, int evaluacionId);
        Task<(bool, string)> Update(List<RespuestasDto> respuestas, int evaluacionId);
        Task<(bool, string)> Delete(int evaluacionId);
    }
}