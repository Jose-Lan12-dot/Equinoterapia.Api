using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class CaballoRepository : ICaballoRepository, ICaballoQueryRepository
    {
        private readonly AppDbContext _context;

        public CaballoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Caballo?> GetByIdAsync(int caballoId)
        {
            return await _context.caballo.FindAsync(caballoId);
        }

        public void Add(Caballo caballo)
        {
            _context.caballo.Add(caballo);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<CaballoDto>> GetAllDtosAsync()
        {
            return await _context.caballo
                   .AsNoTracking()
                   .Select(c => new CaballoDto
                   {
                       CaballoId = c.CaballoId,
                       Placa = c.Placa,
                       Nombre = c.Nombre,
                       Estado = c.Estado.ToString(),
                       FechaHerraje = c.FechaHerraje,
                       Padecimiento = c.Padecimiento,
                   })
                   .ToListAsync();
        }

        public void Delete(Caballo caballo)
        {
            _context.caballo.Remove(caballo);
        }

        public async Task<List<DropdownItemDto>> GetDisponiblesAsync()
        {
            return await _context.caballo
                .Where(c => c.Estado == EstadoCaballo.Disponible)
                .Select(c => new DropdownItemDto
                {
                    Id = c.CaballoId,
                    Nombre = c.Nombre 
                })
                .ToListAsync();
        }
    }
}