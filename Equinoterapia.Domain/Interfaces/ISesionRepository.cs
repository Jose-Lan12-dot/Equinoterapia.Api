using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface ISesionRepository
    {
        Task<Sesion?> GetByIdAsync(int sesionId);
        void Add(Sesion sesion);
        void Delete(Sesion sesion);
        Task<bool> SaveChangesAsync();

        Task<List<Asistencia>> GetAsistenciasBySesionIdAsync(int sesionId);
        void RemoveAsistencias(IEnumerable<Asistencia> asistencias);

        Task<List<Sesion>> GetByCaballoIdAsync(int caballoId);
        Task<List<Sesion>> GetByInstructorIdAsync(int instructorId);
        void RemoveRange(IEnumerable<Sesion> sesiones);

        Task<List<Sesion>> GetByFechaAsync(DateOnly fecha);
    }
}