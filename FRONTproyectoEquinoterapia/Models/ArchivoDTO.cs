using Microsoft.AspNetCore.Components.Forms;

namespace FRONTproyectoEquinoterapia.Models
{
    public class ArchivoDTO
    {
        public IBrowserFile? DiagnosticoMedico { get; set; }
        public IBrowserFile? CertificadoMedico { get; set; }
        public IBrowserFile? ActaNacimiento { get; set; }
        public IBrowserFile? Curp { get; set; }
        public IBrowserFile? ComprobanteDomicilio { get; set; }
        public IBrowserFile? IneTutor { get; set; }
        public IBrowserFile? Fotografia { get; set; }
    }


}


