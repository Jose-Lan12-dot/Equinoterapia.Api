namespace Equinoterapia.Application.DTOs.Evaluaciones
{
    public class AddEvaluacionDto
    {
        public DateOnly Fecha { get; set; }
        public List<RespuestasDto> Respuestas { get; set; } = new List<RespuestasDto>();
    }
}
