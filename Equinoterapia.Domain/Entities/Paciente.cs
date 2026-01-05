using System.ComponentModel;

namespace Equinoterapia.Domain.Entities
{
    public enum EstadoPaciente
    {
        EnTratamiento,
        Baja
    }

    public enum Generos
    {
        Masculino,
        Femenino,
        NoBinario
    }

    public partial class Paciente
    {
        public int PacienteId { get; set; }

        public required string Nombre { get; set; }

        public required DateTime FechaNacimiento { get; set; }

        public required string Tutor { get; set; }

        public required string TelefonoTutor { get; set; }

        public required EstadoPaciente Estado { get; set; }

        public required Generos Genero { get; set; }

        public required DateTime FechaRegistro { get; set; }
        public DateTime? FechaUltimoReingreso { get; set; }



        public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();
        public virtual ICollection<Archivos> Archivos { get; set; } = new List<Archivos>();
        public virtual ICollection<Evaluacion>? Evaluacion { get; set; }

    }

}