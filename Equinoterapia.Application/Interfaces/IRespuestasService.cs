using Equinoterapia.Application.DTOs; 

namespace Equinoterapia.Application.Interfaces
{
    public interface IRespuestasService
    {
        Task<(IEnumerable<RespuestasDto>, string)> Get(int evaluacionId);
        Task<(bool, string)> Add(IEnumerable<RespuestasDto> respuestas, int evaluacionId);
        Task<(bool, string)> Update(IEnumerable<RespuestasDto> respuestas, int evaluacionId);
        Task<(bool, string)> Delete(int evaluacionId);
    }
}