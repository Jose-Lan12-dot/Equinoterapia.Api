namespace APIproyectoEquinoterapia.Data.Models.Paciente_
{
    public class Archivo
    {
        public int ArchivoId { get; set; }
        public int PacienteId { get; set; }
        public byte[]? DiagnosticoMedico { get; set; }
        public byte[]? CertificadoMedico { get; set; }
        public byte[]? ActaNacimiento { get; set; }
        public byte[]? Curp { get; set; }
        public byte[]? ComprobanteDomicilio { get; set; }
        public byte[]? IneTutor { get; set; }
        public byte[]? Fotografia { get; set; }
        public virtual Paciente Paciente { get; set; } = null!;

    }
}
