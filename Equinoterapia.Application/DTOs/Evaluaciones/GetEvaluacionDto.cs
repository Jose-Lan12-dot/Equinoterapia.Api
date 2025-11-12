namespace Equinoterapia.Application.DTOs.Evaluaciones
{
    public class GetEvaluacionDto
    {
        public string nombrePaciente { get; set; } = string.Empty;
        public DateOnly Fecha { get; set; }
        public List<PreguntasDto> Preguntas { get; set; } = new List<PreguntasDto>();
        public List<RespuestasDto> Respuestas { get; set; } = new List<RespuestasDto>();


    }
}
