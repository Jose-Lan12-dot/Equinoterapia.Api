using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IInformeRepository
    {
        Task<InformeMensual?> GetInformeAsync(int mes, int anio);
        void AddInforme(InformeMensual informe);
        void RemoveDetalles(IEnumerable<DetalleInforme> detalles);
        Task<bool> SaveChangesAsync();

        Task<int> GetTotalTerapiasAsync(int mes, int anio);
        Task<int> GetNuevosIngresosAsync(int mes, int anio);
    }
}