namespace Equinoterapia.Domain.Entities
{
    public enum EstadoCaballo
    {
        Disponible,
        EnTratamiento,
        Descansando,
        Retirado
    }


    public partial class Caballo
    {
        public int CaballoId { get; set; }
        public required string Placa { get; set; } = null!;

        public required string Nombre { get; set; } = null!;

        public required EstadoCaballo Estado { get; set; }

        public required DateOnly FechaHerraje { get; set; }


        public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
        public List<HistorialMedico> HistorialMedico { get; set; } = new();

    }
}
