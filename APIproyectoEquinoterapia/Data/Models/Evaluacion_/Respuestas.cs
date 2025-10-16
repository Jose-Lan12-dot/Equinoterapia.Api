using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIproyectoEquinoterapia.Data.Models.Evaluacion_
{
    public class Respuestas
    {
        public int RespuestaId { get; set; }
        public int EvaluacionId { get; set; }
        public int PreguntaId { get; set; }
        public int Respuesta { get; set; }

        public virtual Evaluacion Evaluacion { get; set; } = null!;
        public virtual Preguntas Pregunta { get; set; } = null!;
    }
}
