namespace Equinoterapia.Domain.Entities
{
    public class Preguntas
    {
        public int PreguntaId { get; set; }
        public required string Pregunta { get; set; }
        public virtual ICollection<Respuestas>? Respuestas { get; set; }
    }
}
