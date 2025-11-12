
namespace Equinoterapia.Application.DTOs.Evaluaciones
{
    public class UpdateEvaluacionDto
    {
        public  DateOnly? Fecha { get; set; }
        public List<RespuestasDto>? Respuestas { get; set; }
    }
}
