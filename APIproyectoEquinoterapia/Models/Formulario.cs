namespace APIproyectoEquinoterapia.Models
{
    public class Formulario
    {
        public int FormularioId { get; set; }
        public int PacienteId { get; set; } 
        public DateOnly Fecha { get; set; }
        public virtual Paciente Paciente { get; set; } = null!;
        public virtual ICollection<FormularioRespuestas> Respuestas { get; set; } = new List<FormularioRespuestas>();

    }
}
