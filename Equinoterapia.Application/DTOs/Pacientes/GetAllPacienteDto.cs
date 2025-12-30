namespace Equinoterapia.Application.DTOs.Pacientes
{
    public class GetAllPacienteDto
    {
        public int PacienteId { get; set; }
        public required string Nombre { get; set; }
        public required string Tutor { get; set; }
        public required string Estado { get; set; }
        public required int NumeroFaltas { get; set; }
        public required bool FormularioHecho { get; set; }
        public required bool FaltanArchivos { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
