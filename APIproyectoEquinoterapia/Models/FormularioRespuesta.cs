namespace APIproyectoEquinoterapia.Models
{
    public class FormularioRespuesta
    {
        public int FormularioRespuestaId { get; set; }
        public int FormularioId { get; set; }
        public string? Consulta { get; set; }
        public int Respuesta { get; set; }
        public virtual Formulario Formulario { get; set; } = null!;

    }
}
