namespace FRONTproyectoEquinoterapia.Models
{
    public class GetEvaluacionDto
    {
        public string nombrePaciente { get; set; } = string.Empty;
        public DateOnly Fecha { get; set; }
        public List<PreguntasDto> Preguntas { get; set; } = new();
        public List<RespuestasDto> Respuestas { get; set; } = new();
    }

    public class AddEvaluacionDto
    {
        public DateOnly Fecha { get; set; }
        public List<RespuestasDto> Respuestas { get; set; } = new();
    }

    public class UpdateEvaluacionDto
    {
        public DateOnly? Fecha { get; set; }
        public List<RespuestasDto>? Respuestas { get; set; }
    }

    public class PreguntasDto
    {
        public int Orden { get; set; }
        public string Pregunta { get; set; } = string.Empty;
    }

    public class RespuestasDto
    {
        public int PreguntaId { get; set; }
        public int Respuesta { get; set; }
    }

}
