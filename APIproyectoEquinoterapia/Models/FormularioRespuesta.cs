namespace APIproyectoEquinoterapia.Models
{
    public class FormularioRespuesta
    {
        public int RespuestaId { get; set; }
        public int FormularioId { get; set; }
        public int PreguntaId { get; set; }
        public int Respuesta { get; set; } // O string, según el tipo de respuesta

        public virtual Formulario Formulario { get; set; } = null!;
        public virtual FormularioPregunta Pregunta { get; set; } = null!;
    }
}
