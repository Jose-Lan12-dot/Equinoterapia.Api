namespace APIproyectoEquinoterapia.DTOs
{
    public class ArchivoDTO
    {
        public int PacienteId { get; set; }
        public required String nombre { get; set; }
        public IFormFile? Archivo { get; set; }
    }
}
