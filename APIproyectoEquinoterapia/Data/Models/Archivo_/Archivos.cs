namespace APIproyectoEquinoterapia.Data.Models.Archivo_
{
    public class Archivos
    {
        public int ArchivoId { get; set; }
        public int PacienteId { get; set; }
        public int TipoArchivoId { get; set; }
        public required byte[] Archivo { get; set; }
        public virtual Paciente Paciente { get; set; } = null!;
        public virtual TipoArchivo TipoArchivo { get; set; } = null!;

    }
}
