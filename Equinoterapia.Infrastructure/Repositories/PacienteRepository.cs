using Equinoterapia.Application.DTOs.Pacientes;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence; // El 'using' para tu AppDbContext
using Microsoft.EntityFrameworkCore;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Application.DTOs.Shared;


namespace Equinoterapia.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository, IPacienteQueryRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Paciente paciente)
        {
            _context.paciente.Add(paciente);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // SaveChangesAsync devuelve el número de filas afectadas
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Paciente?> GetByIdAsync(int pacienteId)
        {
            // FindAsync es perfecto para buscar por Clave Primaria
            return await _context.paciente.FindAsync(pacienteId);
        }


        public async Task<int> GetFaltasCountAsync(int pacienteId)
        {
            return await _context.asistencia
                .CountAsync(c => c.PacienteId == pacienteId && c.Asistio == false);
        }

        public async Task<bool> HasEvaluacionAsync(int pacienteId)
        {
            return await _context.evaluacion
                .AnyAsync(f => f.PacienteId == pacienteId);
        }

        public async Task<int> GetArchivosCountAsync(int pacienteId)
        {
            return await _context.archivo
               .CountAsync(a => a.PacienteId == pacienteId);
        }

        public async Task<int> GetTotalFileTypesCountAsync()
        {
            // Asumo que tu DbContext tiene 'tipoArchivo'
            return await _context.tipoArchivo.CountAsync();
        }

        public async Task<List<string>> GetMissingFileTypesAsync(int pacienteId)
        {
            // Asumo que 'TiposArchivo' es la entidad 'TipoArchivo'
            // y que 'paciente.Archivos' es la colección
            var archivosPaciente = await _context.archivo
                .Where(a => a.PacienteId == pacienteId)
                .Select(a => a.TiposArchivo)
                .ToListAsync();

            var archivosRequeridos = await _context.tipoArchivo.ToListAsync();

            var archivosFaltantes = archivosRequeridos
                .Where(ar => !archivosPaciente.Contains(ar))
                .Select(ar => ar.Nombre) // Asumo que TipoArchivo tiene 'Nombre'
                .ToList();

            return archivosFaltantes;
        }

        public async Task<List<GetAllPacienteDto>> GetAllAsync()
        {
            var numeroArchivosRequeridos = await _context.tipoArchivo.CountAsync();

            // Esta es la query optimizada
            return await _context.paciente
                .AsNoTracking() // Mejora rendimiento, no necesitamos "vigilar" estos datos
                .Select(p => new GetAllPacienteDto
                {
                    // Mapeo directo
                    PacienteId = p.PacienteId,
                    Nombre = p.Nombre,
                    Tutor = p.Tutor,
                    Estado = p.Estado.ToString(),

                    // LINQ dentro de LINQ. EF Core lo traduce a Sub-Queries de SQL.
                    NumeroFaltas = _context.asistencia
                        .Count(a => a.PacienteId == p.PacienteId && a.Asistio == false),

                    FormularioHecho = _context.evaluacion
                        .Any(e => e.PacienteId == p.PacienteId),

                    FaltanArchivos = (_context.archivo
                        .Count(ar => ar.PacienteId == p.PacienteId) < numeroArchivosRequeridos)
                })
                .ToListAsync(); // TODO EL CÓDIGO ANTERIOR SE EJECUTA EN LA BD
        }

        public void Delete(Paciente paciente)
        {
            _context.paciente.Remove(paciente);
        }

        public async Task<List<DropdownItemDto>> GetActivosAsync()
        {
            return await _context.paciente
                .Where(p => p.Estado == EstadoPaciente.Activo || p.Estado == EstadoPaciente.TratamientoCompleto)
                .Select(p => new DropdownItemDto
                {
                    Id = p.PacienteId,
                    Nombre = p.Nombre
                })
                .ToListAsync();
        }



    }
}