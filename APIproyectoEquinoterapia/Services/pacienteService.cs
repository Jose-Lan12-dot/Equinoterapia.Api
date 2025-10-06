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

        public async Task<(IEnumerable<PacienteDTO>,string)> GetPacientesAsync()
        {
            var pacientes = await _context.paciente
                            .Select(p => new PacienteDTO
                            {
                                PacienteId = p.PacienteId,
                                Nombre = p.Nombre,
                                Tutor = p.Tutor,
                                Estado = p.Estado.ToString(),
                                FechaRegistro = p.FechaRegistro,
                                NumeroFaltas = _context.cita
                                    .Count(c => c.PacienteId == p.PacienteId && c.Asistio == false),
                                FormularioHecho = _context.formulario.Any(f => f.PacienteId == p.PacienteId)
                            })
                            .ToListAsync();

            if(pacientes == null)
            {
                var mensaje = "No se econtro ningun elemto en pacientes";
                return (null, mensaje);
            }

            

            return (pacientes, "Pacientes devueltos con exito");
        }

        public async Task<(PacienteDTO?,string)> GetPacienteAsync(int pacienteId)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);
            if (paciente == null)
            {
                var mensaje = "Paiente no encontrado";
                return (null,mensaje);
            }
               

            return (new PacienteDTO
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                FechaNacimiento = paciente.FechaNacimiento,
                Tutor = paciente.Tutor,
                TelefonoTutor = paciente.TelefonoTutor,
                Estado = paciente.Estado.ToString(),
                Genero = paciente.Genero.ToString(),
                FechaRegistro = paciente.FechaRegistro,
                NumeroFaltas = _context.cita
                                    .Count(c => c.PacienteId == paciente.PacienteId && c.Asistio == false),
                FormularioHecho = _context.formulario.Any(f => f.PacienteId == paciente.PacienteId)
            },"Paciente encontrado");
        }

        public async Task<(bool,string)> PutPacienteAsync(int pacienteId, PacienteDTO pacienteDTO)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);
            if (paciente == null)
            {
                var mensaje = "Paciente no encontrado";
                return (false,mensaje);
            }

            if (Enum.TryParse<EstadoPaciente>(pacienteDTO.Estado, out var estadoValido)
                && Enum.TryParse<Generos>(pacienteDTO.Genero,out var generoValido))
            {
                paciente.Estado = estadoValido;
                paciente.Genero = generoValido;
            }
            else
            {
                var mensaje = "Genero o estado no validos";
                return (false,mensaje); 
            }



            paciente.Nombre = pacienteDTO.Nombre;
            paciente.FechaNacimiento = pacienteDTO.FechaNacimiento;
            paciente.Tutor = pacienteDTO.Tutor;
            paciente.TelefonoTutor = pacienteDTO.TelefonoTutor;



            await _context.SaveChangesAsync();
            return (true,"Paciente actualizado de forma exitosa");
        }


        public async Task<(bool,string)> PostPacienteAsync (PacienteConArchivosDTO pacienteconarchivosDTO)
        {
            if (_context.paciente.Any(p => p.PacienteId == pacienteconarchivosDTO.PacienteId))
            {
                var mensaje = "El paciente ya existe";
                return (false, mensaje);
            }

            if (!Enum.TryParse<Generos>(pacienteconarchivosDTO.Genero, out var generoValido))
            {
                var mensaje = "Genero o estado no validos";
                return (false, mensaje);
            }

            var nuevoPaciente = new Paciente
            {
                Nombre = pacienteconarchivosDTO.Nombre,
                FechaNacimiento = pacienteconarchivosDTO.FechaNacimiento,
                Tutor = pacienteconarchivosDTO.Tutor,
                TelefonoTutor = pacienteconarchivosDTO.TelefonoTutor,
                Genero = pacienteconarchivosDTO.Genero == null ? Generos.NoBinario : generoValido,
                DiagnosticoMedico = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.DiagnosticoMedico),
                CertificadoMedico = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.CertificadoMedico),
                ActaNacimiento = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.ActaNacimiento),
                Curp = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.Curp),
                ComprobanteDomicilio = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.ComprobanteDomicilio),
                IneTutor = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.IneTutor),
                Fotografia = await ConvertirArchivoABytesAsync(pacienteconarchivosDTO.Fotografia),
                Estado = EstadoPaciente.Activo,
            };

            _context.paciente.Add(nuevoPaciente);
            await _context.SaveChangesAsync();
            return (true,"Agregado Correctamente");
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


        public async Task<(bool, string)> DeletePacienteAsync(int pacienteId)
        {
            var paciente = await _context.paciente.FindAsync(pacienteId);
            if (paciente == null)
            {
                var mensaje = "Paciente no encontrado";
                return (false, mensaje);
            }

            _context.paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return (true, "Se elimino correctamente");
        }




    }
}