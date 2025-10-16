namespace APIproyectoEquinoterapia.DTOs.Evaluacion_
{
    public class EvaluacionDTO
    {
        public string? NombrePaciente { get; set; }
        public DateOnly?Fecha { get; set; }
        public ICollection<PreguntasDTO>? Preguntas { get; set; }
        public ICollection<RespuestasDTO>? Respuestas { get; set; }


    }
}
