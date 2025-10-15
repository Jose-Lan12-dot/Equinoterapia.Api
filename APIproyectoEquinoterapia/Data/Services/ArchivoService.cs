using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Data.Services
{
    public class ArchivoService
    {

        private readonly AppDbContext _context;


        public ArchivoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(byte[]?,string)> GetArchivoAsync(string archivoRequerido, int pacienteId)
        {
            var archivo = await _context.archivo.FirstOrDefaultAsync(a => a.PacienteId == pacienteId);
            if (archivo == null)
            {
                var mensaje = "Archivos del pacinete no encontrado";
                return (null,mensaje);
            }

            var propiedad = typeof(Archivo).GetProperty(archivoRequerido);
            if (propiedad == null)
            {
                var mensaje = "Propiedad no encontrado";
                return (null,mensaje);
            }

            var valor = propiedad.GetValue(archivo) as byte[];
            if (valor == null)
            {
                var mensaje = "Archivo no encontrado";
                return (null,mensaje);
            }

            return (valor,"Archivo encontrado");
        }


        public async Task<(bool,string)> PutArchivoAsync(int pacienteId, string archivoRequerido, IFormFile nuevoArchivo)
        {

            var archivo = await _context.archivo.FirstOrDefaultAsync(a => a.PacienteId == pacienteId);
            if (archivo == null)
            {
                var mensaje = "Archivos del paciente no encontrado";
                return (false,mensaje);
            }

            var propiedad = typeof(Archivo).GetProperty(archivoRequerido);
            if (propiedad == null)
            {
                var mensaje = "Propiedad no encontrado";
                return (false,mensaje);
            }

            var nuevoArchivoByte = await ConvertirArchivoABytesAsync(nuevoArchivo);

            propiedad.SetValue(archivo, nuevoArchivoByte);

            await _context.SaveChangesAsync();
            return (true,archivoRequerido + " Actualizado Correctamnete");
        }

        public async Task<(bool,string)> PostArchivoAsync(ArchivoDTO archivoDTO, int pacienteId)
        {
            if(_context.archivo.Any(a => a.PacienteId == pacienteId)) 
            {
                var mensaje = "Archivos del paciente ya existen";
                return (false,mensaje);
            }
            
            var nuevoArchivo = new Archivo
            {
                PacienteId = pacienteId,
                DiagnosticoMedico = await ConvertirArchivoABytesAsync(archivoDTO.DiagnosticoMedico),
                CertificadoMedico = await ConvertirArchivoABytesAsync(archivoDTO.CertificadoMedico),
                ActaNacimiento = await ConvertirArchivoABytesAsync(archivoDTO.ActaNacimiento),
                Curp = await ConvertirArchivoABytesAsync(archivoDTO.Curp),
                ComprobanteDomicilio = await ConvertirArchivoABytesAsync(archivoDTO.ComprobanteDomicilio),
                IneTutor = await ConvertirArchivoABytesAsync(archivoDTO.IneTutor),
                Fotografia = await ConvertirArchivoABytesAsync(archivoDTO.Fotografia),
            };

            _context.archivo.Add(nuevoArchivo);
            await _context.SaveChangesAsync();

            return (true, "Archivos del paciente " + pacienteId + " Agregados correctamnete");
        }


        public async Task<(bool,string)> DeleteArchivoAsync(int pacienteId, string archivoRequerido)
        {
            var archivo = await _context.archivo.FirstOrDefaultAsync(a => a.PacienteId == pacienteId);
            if (archivo == null)
            {
                var mensaje = "Archivos del paciente no encontrados";
                return (false,mensaje);
            }

            var propiedad = typeof(Archivo).GetProperty(archivoRequerido);
            if(propiedad == null) 
            {
                var mensaje = "Propiedad no encontrado";
                return (false,mensaje);
            }

            propiedad.SetValue(archivo, null);
            return (true,archivoRequerido+"eliminado correctamnete");
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
