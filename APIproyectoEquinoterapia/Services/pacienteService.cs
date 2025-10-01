using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Services
{
    public class PacienteService
    {
        private readonly AppDbContext _context;

        public PacienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PacienteConFaltasDTO>> GetPacientesAsync()
        {
            return await _context.paciente
                            .Select(p => new PacienteConFaltasDTO
                            {
                                PacienteId = p.PacienteId,
                                Nombre = p.Nombre,
                                FechaNacimiento = p.FechaNacimiento,
                                Tutor = p.Tutor,
                                TelefonoTutor = p.TelefonoTutor,
                                FechaRegistro = p.FechaRegistro,
                                NumeroFaltas = _context.cita
                                    .Count(c => c.PacienteId == p.PacienteId && c.Asistio == false)
                            })
                            .ToListAsync();
        }

        public async Task<PacienteConFaltasDTO?> GetPacienteAsync(int id)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return null;

            return new PacienteConFaltasDTO
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                FechaNacimiento = paciente.FechaNacimiento,
                Tutor = paciente.Tutor,
                TelefonoTutor = paciente.TelefonoTutor,
                FechaRegistro = paciente.FechaRegistro,
                NumeroFaltas = _context.cita
                                    .Count(c => c.PacienteId == paciente.PacienteId && c.Asistio == false)
            };
        }

        public async Task<bool> PutPacienteAsync(int id, PacienteDTO pacienteDTO)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return false;

            paciente.Nombre = pacienteDTO.Nombre;
            paciente.FechaNacimiento = pacienteDTO.FechaNacimiento;
            paciente.Tutor = pacienteDTO.Tutor;
            paciente.TelefonoTutor = pacienteDTO.TelefonoTutor;
            paciente.FechaRegistro = pacienteDTO.FechaRegistro;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePacienteAsync(int id)
        {
            var paciente = await _context.paciente.FindAsync(id);
            if (paciente == null)
                return false;

            _context.paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> PostPacienteAsync (PacienteConArchivosDTO pacienteconarchivosDTO)
        {
            if (_context.paciente.Any(p => p.PacienteId == pacienteconarchivosDTO.PacienteId))
                return false;

            var nuevoPaciente = new Paciente
            {
                Nombre = pacienteconarchivosDTO.Nombre,
                FechaNacimiento = pacienteconarchivosDTO.FechaNacimiento,
                Tutor = pacienteconarchivosDTO.Tutor,
                TelefonoTutor = pacienteconarchivosDTO.TelefonoTutor,
                DiagnosticoMedico = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.DiagnosticoMedico),
                CertificadoMedico = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.CertificadoMedico),
                ActaNacimiento = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.ActaNacimiento),
                Curp = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.Curp),
                ComprobanteDomicilio = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.ComprobanteDomicilio),
                IneTutor = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.IneTutor),
                Fotografia = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.Fotografia),
            };

            _context.paciente.Add(nuevoPaciente);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<byte[]?> GetArchivoAsync(string archivoRequerido, int pacienteId)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);

            var propiedad = typeof(Paciente).GetProperty(archivoRequerido);
            if (propiedad == null)
                return null;

            var valor = propiedad.GetValue(paciente) as byte[];
            return valor;
        }


        public async Task<bool> PutArchivoAsync(int pacienteId, string archivoRequerido, IFormFile nuevoArchivo)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);

            var propiedad = typeof(Paciente).GetProperty(archivoRequerido);
            if (propiedad == null)
                return false;

            var nuevoArchivoByte = ConvertirArchivoABytesAsync (nuevoArchivo);

            propiedad.SetValue(paciente, nuevoArchivoByte);

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteArchivoAsync(int pacienteId, string archivoRequerido)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);
            var propiedad = typeof(Paciente).GetProperty(archivoRequerido);
            if (propiedad == null)
                return false;
            propiedad.SetValue(paciente, null);
            return true;
        }


        private async Task<byte[]?> ConvertirArchivoABytesAsync(IFormFile? archivo)
        {
            if (archivo == null) return null;
            using var ms = new MemoryStream();
            await archivo.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}