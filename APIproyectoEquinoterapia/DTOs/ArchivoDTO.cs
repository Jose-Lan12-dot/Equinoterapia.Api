namespace APIproyectoEquinoterapia.DTOs
{
    public class ArchivoDTO
    {
        public int PacienteId { get; set; }
        public IFormFile? DiagnosticoMedico { get; set; }
        public IFormFile? CertificadoMedico { get; set; }
        public IFormFile? ActaNacimiento { get; set; }
        public IFormFile? Curp { get; set; }
        public IFormFile? ComprobanteDomicilio { get; set; }
        public IFormFile? IneTutor { get; set; }
        public IFormFile? Fotografia { get; set; }
    }
}
