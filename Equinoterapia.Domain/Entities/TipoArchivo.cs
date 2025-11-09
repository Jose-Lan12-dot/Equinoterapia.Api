namespace Equinoterapia.Domain.Entities
{
    public class TipoArchivo
    {
        public int TipoId { get; set; }
        public required string Nombre { get; set; }
        public required string Extension { get; set; }
        public virtual ICollection<Archivos> Archivos { get; set; } = new List<Archivos>();
    }
}
