using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.Data.Models.Archivo_;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace APIproyectoEquinoterapia.Data.Services.Paciente_
{
    public class ArchivoService
    {

        private readonly AppDbContext _context;


        public ArchivoService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<(byte[]?,string,string)> GetArchivoAsync(string nombreArchivo, int pacienteId)
        {
            var tipoArchivo = await _context.tipoArchivos.
                FirstOrDefaultAsync(t => t.Nombre == nombreArchivo);

            if(EsNulo(tipoArchivo))
            {
                var mensaje = "Tipo de archivoActual no encontrado";
                return (null,mensaje,"");
            }


            var archivo = await _context.archivo
                .Where(a => a.PacienteId == pacienteId && a.TipoArchivoId == tipoArchivo!.TipoArchivoId)
                .Select(p => p.Archivo)
                .FirstOrDefaultAsync();

            if (EsNulo(archivo))
            {
                var mensaje = "Archivo no encontrado";
                return (null,mensaje,"");
            }

            return (archivo,"Archivos encontrado",tipoArchivo.Extension);
        }


        public async Task<(bool,string)> PutArchivoAsync(ArchivoDTO archivoDTO)
        {
            if (EsNulo(archivoDTO.Archivo))
            {
                var mensaje = "Sin Archivo";
                return (false, mensaje);
            }

            var tipoArchivoId = await ObtenerTipoArchivoId(archivoDTO.nombre);

            if (EsNulo(tipoArchivoId))
            {
                var mensaje = "Tipo de archivoActual no aceptado";
                return (false, mensaje);
            }

            var archivoActual = await _context.archivo
                .Where(a => a.PacienteId == archivoDTO.PacienteId
                && a.TipoArchivoId == tipoArchivoId)
                .Select(a => a.Archivo)
                .FirstOrDefaultAsync();

            if (EsNulo(archivoActual))
            {
                var mensaje = "Archivo del paciente no existe";
                return (false, mensaje);
            }

            archivoActual = await ConvertirArchivoABytesAsync(archivoDTO.Archivo);
            await _context.SaveChangesAsync();

            return (true,archivoDTO.nombre + " Actualizado Correctamnete");
        }

        public async Task<(bool,string)> PostArchivoAsync(ArchivoDTO archivoDTO)
        {
            if (EsNulo(archivoDTO.Archivo))
            {
                var mensaje = "Sin Archivo";
                return (false,mensaje);
            }

            var tipoArchivoId = await ObtenerTipoArchivoId(archivoDTO.nombre);
            if (EsNulo(tipoArchivoId))
            {
                var mensaje = "Tipo de archivoActual no aceptado";
                return (false, mensaje);
            }

            if (!await ArchivoExiste(archivoDTO.PacienteId, tipoArchivoId)) 
            {
                var mensaje = "Archivos del paciente ya existen";
                return (false,mensaje);
            }
            
            var nuevoArchivo = new Archivos
            {
                PacienteId = archivoDTO.PacienteId,
                TipoArchivoId = tipoArchivoId ?? 0,
                Archivo = await ConvertirArchivoABytesAsync(archivoDTO.Archivo)
            };

            _context.archivo.Add(nuevoArchivo);
            await _context.SaveChangesAsync();

            return (true, archivoDTO.nombre + " Agregado correctamnete");
        }


        public async Task<(bool,string)> DeleteArchivoAsync(int pacienteId, string nombreArchivo)
        {

            var tipoArchivoId = await ObtenerTipoArchivoId(nombreArchivo);

            if (EsNulo(tipoArchivoId))
            {
                var mensaje = "Tipo de archivoActual no encontrado";
                return (false, mensaje);
            }

            var archivo = await _context.archivo
                .Where(a => a.PacienteId == pacienteId
                && a.TipoArchivoId == tipoArchivoId)
                .FirstOrDefaultAsync();

            if (EsNulo(archivo))
            {
                var mensaje = "Archivo no encontrado";
                return (false, mensaje);
            }

            _context.archivo.Remove(archivo!);
            await _context.SaveChangesAsync();

            return (true, nombreArchivo + " eliminado correctamente");
        }



        private async Task<byte[]?> ConvertirArchivoABytesAsync(IFormFile? archivo)
        {
            using var ms = new MemoryStream();
            await archivo!.CopyToAsync(ms);
            return ms.ToArray();
        }

        private bool EsNulo(object? objeto)
        {
            if (objeto == null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> ArchivoExiste(int pacienteId, int? tipoArchivoId)
        {
            return await _context.archivo.AnyAsync(a => a.PacienteId == pacienteId && a.TipoArchivoId == tipoArchivoId);
        }
        private async Task<int?> ObtenerTipoArchivoId(string nombreArchivo)
        {
            return await _context.tipoArchivos
                .Where(a => a.Nombre == nombreArchivo)
                .Select(a => a.TipoArchivoId)
                .FirstOrDefaultAsync();
        }
    }
}
