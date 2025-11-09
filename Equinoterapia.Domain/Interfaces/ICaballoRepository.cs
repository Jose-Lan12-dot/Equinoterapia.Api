using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface ICaballoRepository
    {
        Task<Caballo?> GetByIdAsync(int caballoId);
        void Add(Caballo caballo);
        Task<bool> SaveChangesAsync();

        void Delete(Caballo caballo); 
    }
}