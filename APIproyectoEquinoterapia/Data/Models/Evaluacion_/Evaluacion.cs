using APIproyectoEquinoterapia.Data.Models.Paciente_;
using System.ComponentModel.DataAnnotations;
namespace APIproyectoEquinoterapia.Data.Models.Evaluacion_
{
    public class Evaluacion
    {
        public int EvaluacionId { get; set; }
        public int PacienteId { get; set; } 
        public DateOnly? Fecha { get; set; }

        public virtual Paciente Paciente { get; set; } = null!;
        public virtual ICollection<Respuestas> Respuestas { get; set; } = new List<Respuestas>();
    }
}
