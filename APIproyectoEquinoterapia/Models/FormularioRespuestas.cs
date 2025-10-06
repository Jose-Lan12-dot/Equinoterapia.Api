namespace APIproyectoEquinoterapia.Models
{
    public class FormularioRespuestas
    {
        public int RespuestaId { get; set; }
        public int FormularioId { get; set; }
        public int PreguntaId { get; set; }
        public int Respuesta { get; set; }

        public virtual Formulario Formulario { get; set; } = null!;
        public virtual FormularioPreguntas Pregunta { get; set; } = null!;
    }
}
