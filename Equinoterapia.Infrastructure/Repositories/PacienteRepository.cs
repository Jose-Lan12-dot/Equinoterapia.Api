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

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Paciente?> GetByIdAsync(int pacienteId)
        {
        
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

            return await _context.tipoArchivo.CountAsync();
        }

        public async Task<List<string>> GetMissingFileTypesAsync(int pacienteId)
        {

            var archivosPaciente = await _context.archivo
                .Where(a => a.PacienteId == pacienteId)
                .Select(a => a.TiposArchivo)
                .ToListAsync();

            var archivosRequeridos = await _context.tipoArchivo.ToListAsync();

            var archivosFaltantes = archivosRequeridos
                .Where(ar => !archivosPaciente.Contains(ar))
                .Select(ar => ar.Nombre) 
                .ToList();

            return archivosFaltantes;
        }

        public async Task<List<GetAllPacienteDto>> GetAllAsync()
        {
            var numeroArchivosRequeridos = await _context.tipoArchivo.CountAsync();

            var datosCrudos = await _context.paciente
                .AsNoTracking()
                .Select(p => new
                {
                    p.PacienteId,
                    p.Nombre,
                    p.Tutor,
                    EstadoEnum = p.Estado, 

                   
                    NumeroFaltas = _context.asistencia
                        .Count(a => a.PacienteId == p.PacienteId && a.Asistio == false),

                    FormularioHecho = _context.evaluacion
                        .Any(e => e.PacienteId == p.PacienteId),

                    FaltanArchivos = (_context.archivo
                        .Count(ar => ar.PacienteId == p.PacienteId) < numeroArchivosRequeridos)
                })
                .ToListAsync();

            var resultadoFinal = datosCrudos.Select(item => new GetAllPacienteDto
            {
                PacienteId = item.PacienteId,
                Nombre = item.Nombre,
                Tutor = item.Tutor,

                
                Estado = ObtenerDescripcionEstado(item.EstadoEnum),

                NumeroFaltas = item.NumeroFaltas,
                FormularioHecho = item.FormularioHecho,
                FaltanArchivos = item.FaltanArchivos
            }).ToList();

            return resultadoFinal;



        }


        // Método privado dentro de PacienteRepository.cs
        private string ObtenerDescripcionEstado(EstadoPaciente estado)
        {
            return estado switch
            {
                EstadoPaciente.EnTratamiento => "En Tratamiento", 
                EstadoPaciente.Baja => "Baja",
                _ => estado.ToString() 
            };
        }



        public void Delete(Paciente paciente)
        {
            _context.paciente.Remove(paciente);
        }

        public async Task<List<DropdownItemDto>> GetActivosAsync()
        {
            return await _context.paciente
                .Where(p =>  p.Estado == EstadoPaciente.EnTratamiento)
                .Select(p => new DropdownItemDto
                {
                    Id = p.PacienteId,
                    Nombre = p.Nombre
                })
                .ToListAsync();
        }

        public async Task<Enum> PacienteEstado(int pacienteId)
        {
            return await _context.paciente
                .Where(p => p.PacienteId == pacienteId)
                .Select(p => p.Estado)
                .FirstOrDefaultAsync();
        }





    }
}