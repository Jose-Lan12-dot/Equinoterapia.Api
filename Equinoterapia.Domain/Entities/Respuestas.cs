namespace Equinoterapia.Domain.Entities
{
    public class Respuestas
    {
        public int RespuestaId { get; set; }
        public required int EvaluacionId { get; set; }
        public required int PreguntaId { get; set; }
        public required int Respuesta { get; set; }

        public virtual Evaluacion Evaluacion { get; set; } = null!;
        public virtual Preguntas Pregunta { get; set; } = null!;
    }
}
