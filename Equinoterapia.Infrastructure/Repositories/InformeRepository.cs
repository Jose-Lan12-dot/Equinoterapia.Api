using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class InformeRepository : IInformeRepository
    {
        private readonly AppDbContext _context;

        public InformeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<InformeMensual?> GetInformeAsync(int mes, int anio)
        {
            return await _context.informeMensual
                .Include(i => i.Detalles.OrderBy(d => d.NumeroOrden))
                .FirstOrDefaultAsync(i => i.Mes == mes && i.Anio == anio);
        }

        public void AddInforme(InformeMensual informe)
        {
            _context.informeMensual.Add(informe);
        }

        public void RemoveDetalles(IEnumerable<DetalleInforme> detalles)
        {
            _context.DetalleInforme.RemoveRange(detalles);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }





        // --- CONSULTAS ESTADÍSTICAS ---
        public async Task<int> GetTotalTerapiasAsync(int mes, int anio)
        {
            return await _context.sesion
                .Where(s => s.Fecha.Month == mes &&
                            s.Fecha.Year == anio)
                .CountAsync();

            //EN CASO DE QUERER TERAPIAS INDIVIALMENTE Y NO POR GRUPOS
            //return await _context.asistencia
            //    .Where(a => a.Sesion.Fecha.Month == mes &&
            //                a.Sesion.Fecha.Year == anio &&
            //                a.Asistio == true)
            //    .CountAsync();


        }

        public async Task<int> GetNuevosIngresosAsync(int mes, int anio)
        {
            return await _context.paciente
                .Where(p => p.FechaRegistro.Month == mes &&
                            p.FechaRegistro.Year == anio)
                .CountAsync();
        }
    }
}