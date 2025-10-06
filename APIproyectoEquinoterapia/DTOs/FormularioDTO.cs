namespace APIproyectoEquinoterapia.DTOs
{
    public class FormularioDTO
    {
        public string? NombrePaciente { get; set; }
        public DateOnly Fecha { get; set; }
        public ICollection<FormularioPreguntaDTO>? Preguntas { get; set; }
        public ICollection<FormularioRespuestaDTO>? Respuestas { get; set; }


    }
}
