namespace Equinoterapia.Domain.Entities
{
    public partial class Asistencia
    {
        public int AsistenciaId { get; set; }

        public required int SesionId { get; set; }

        public required int PacienteId { get; set; }

        public required bool Asistio { get; set; }

        public virtual Paciente Paciente { get; set; } = null!;

        public virtual Sesion Sesion { get; set; } = null!;
    }
}
