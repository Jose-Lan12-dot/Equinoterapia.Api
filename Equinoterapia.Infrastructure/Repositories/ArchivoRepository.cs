using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly AppDbContext _context;

        public ArchivoRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- Métodos de Lectura ---

        public async Task<TipoArchivo?> GetTipoArchivoPorNombreAsync(string nombreArchivo)
        {
            return await _context.tipoArchivo
                .FirstOrDefaultAsync(t => t.Nombre == nombreArchivo);
        }

        public async Task<Archivos?> GetArchivoAsync(int pacienteId, int tipoId)
        {
            return await _context.archivo
                .FirstOrDefaultAsync(a => a.PacienteId == pacienteId && a.TipoId == tipoId);
        }

        public async Task<bool> PacienteExistsAsync(int pacienteId)
        {
            return await _context.paciente
                .AnyAsync(p => p.PacienteId == pacienteId);
        }

        public async Task<bool> ArchivoExistsAsync(int tipoId, int pacienteId)
        {
            return await _context.archivo
                .AnyAsync(a => a.TipoId == tipoId && a.PacienteId == pacienteId);
        }

        public async Task<int> GetTipoIdPorNombreAsync(string nombreArchivo)
        {
            return await _context.tipoArchivo
                .Where(t => t.Nombre == nombreArchivo)
                .Select(t => t.TipoId)
                .FirstOrDefaultAsync();
        }

        // --- Métodos de Escritura ---

        public void Add(Archivos archivo)
        {
            _context.archivo.Add(archivo);
        }

        public void Delete(Archivos archivo)
        {
            _context.archivo.Remove(archivo);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Archivos>> GetByPacienteIdAsync(int pacienteId)
        {
            return await _context.archivo
                .Where(a => a.PacienteId == pacienteId)
                .ToListAsync();
        }

        public void RemoveRange(IEnumerable<Archivos> archivos)
        {
            _context.archivo.RemoveRange(archivos);
        }
    }
}