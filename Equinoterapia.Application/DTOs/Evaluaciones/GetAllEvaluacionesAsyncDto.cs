
namespace Equinoterapia.Application.DTOs.Evaluaciones
{
    internal class GetAllEvaluacionesAsyncDto
    {
        List<GetEvaluacionDto> Evaluaciones { get; set; } = new List<GetEvaluacionDto>();
    }
}
