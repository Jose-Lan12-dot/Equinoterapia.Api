using System.ComponentModel.DataAnnotations;

namespace APIproyectoEquinoterapia.Data.Models.Evaluacion_
{
    public class Preguntas
    {
        public int PreguntaId { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public virtual ICollection<Respuestas>? Respuestas { get; set; }
    }
}
