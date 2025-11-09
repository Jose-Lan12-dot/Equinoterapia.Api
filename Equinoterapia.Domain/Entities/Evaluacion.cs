namespace Equinoterapia.Domain.Entities
{
    public class Evaluacion
    {
        public int EvaluacionId { get; set; }
        public required int PacienteId { get; set; } 
        public required DateOnly Fecha { get; set; }

        public virtual Paciente Paciente { get; set; } = null!;
        public virtual ICollection<Respuestas> Respuestas { get; set; } = new List<Respuestas>();
    }
}
