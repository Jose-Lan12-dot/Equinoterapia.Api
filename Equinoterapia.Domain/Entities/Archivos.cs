namespace Equinoterapia.Domain.Entities
{
    public class Archivos
    {
        public int ArchivoId { get; set; }
        public required int PacienteId { get; set; }
        public required int TipoId { get; set; }
        public required byte[] Archivo { get; set; }



        public virtual Paciente Paciente { get; set; } = null!;
        public virtual TipoArchivo TiposArchivo { get; set; } = null!;

    }
}
