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


//////////////////////
        public async Task<(byte[]?,string,string)> Getsync(string nombreArchivo, int pacienteId)
        {
            var tipoArchivo = await GetTipoArchivo(nombreArchivo);

            if (IsNull(tipoArchivo))
            {
                var mensaje = "Tipo de archivo no encontrado";
                return (null,mensaje,"");
            }


            var archivo = await GetArchivo(pacienteId, tipoArchivo!.TipoId);

            if (IsNull(archivo))
            {
                var mensaje = "Archivo no encontrado";
                return (null,mensaje,"");
            }

            return (archivo!.Archivo,"Archivos encontrado",tipoArchivo!.Extension);
        }

//////////////////////
        public async Task<(bool, string)> AddAsync(ArchivoDto archivos, int pacienteId)
        {
            if (ValidateFileData(archivos))
            {
                var mensaje = "Entrada no valida";
                return (false, mensaje);
            }

            var tipoId = await GetTipoId(archivos.Nombre!);
            if (tipoId == 0)
            {
                var mensaje = "Tipo de archivo no permitido";
                return (false, mensaje);
            }

            if (!await PacienteExist(pacienteId))
            {
                var mensaje = "El paciente no existe";
                return (false, mensaje);
            }

            if (await ArchivoExist(tipoId, pacienteId))
            {
                var mensaje = "Archivo del paciente ya existe";
                return (false, mensaje);
            }

            var nuevoArchivo = new Archivos
            {
                PacienteId = pacienteId,
                TipoId = tipoId,
                Archivo = await FileToArrayAsync(archivos.Archivo!)
            };

            try
            {
                _context.archivo.Add(nuevoArchivo);
                await _context.SaveChangesAsync();
                return (true, archivos.Nombre + " Agregado correctamnete");
            }
            catch (Exception ex)
            {
                var mensaje = "Error al agregar el archivo: " + ex.Message;
                return (false, mensaje);
            }
        }

//////////////////////
        public async Task<(bool,string)> UpdateAsync(ArchivoDto archivoDto, int pacienteId)
        {
            if (ValidateFileData(archivoDto))
            {
                var mensaje = "Entrada no valida";
                return (false, mensaje);
            }

            var tipoId = await GetTipoId(archivoDto.Nombre!);
            if (tipoId == 0)
            {
                var mensaje = "Tipo de archivo no permitido";
                return (false, mensaje);
            }

            var archivo = await GetArchivo(pacienteId, tipoId);
            if (IsNull(archivo))
            {
                var mensaje = "Archivo del paciente no existe";
                return (false, mensaje);
            }

            try
            { 
            archivo!.Archivo = await FileToArrayAsync(archivoDto.Archivo!);
            await _context.SaveChangesAsync();
            return (true,archivoDto.Nombre + " Actualizado Correctamnete");
            }
            catch (Exception ex)
            {
                var mensaje = "Error al actualizar el archivo: " + ex.Message;
                return (false, mensaje);
            }
        }

//////////////////////
        public async Task<(bool,string)> DeleteAsync( string nombreArchivo, int pacienteId)
        {

            
            var tipoId = await GetTipoId(nombreArchivo);
            if (tipoId == 0)
            {
                var mensaje = "Tipo de archivo no permitido";
                return (false, mensaje);
            }

            var archivo = await GetArchivo(pacienteId, tipoId);
            if (IsNull(archivo))
            {
                var mensaje = "Archivo del paciente no existe";
                return (false, mensaje);
            }


            _context.archivo.Remove(archivo!);
            await _context.SaveChangesAsync();

            return (true, nombreArchivo + " eliminado correctamente");
        }









//////////////////////
        private async Task<bool> PacienteExist(int pacienteId)
        {
            return await _context.paciente
                .AnyAsync(p => p.PacienteId == pacienteId);
        }

//////////////////////
        private async Task<bool> ArchivoExist(int tipoId,int pacienteId)
        {
            return await _context.archivo
                .AnyAsync(a => a.TipoId == tipoId && a.PacienteId == pacienteId);
        }

//////////////////////
        private async Task<Archivos?> GetArchivo(int pacienteId, int tipoId)
        {
            return await _context.archivo
                .FirstOrDefaultAsync(a => a.PacienteId == pacienteId && a.TipoId == tipoId);
        }

//////////////////////
        private async Task<TipoArchivo?> GetTipoArchivo(string nombreArchivo)
        {
            return await _context.tipoArchivo
                .FirstOrDefaultAsync(t => t.Nombre == nombreArchivo);
        }

//////////////////////
        private async Task<int> GetTipoId(string nombreArchivo)
        {
            return await _context.tipoArchivo
                .Where(t => t.Nombre == nombreArchivo)
                .Select(t => t.TipoId)
                .FirstOrDefaultAsync();
        }

//////////////////////
        private async Task<byte[]> FileToArrayAsync(IFormFile archivo)
        {
            using var ms = new MemoryStream();
            await archivo.CopyToAsync(ms);
            return ms.ToArray();
        }




//////////////////////
        private bool ValidateFileData(ArchivoDto? archivoDto)
        {
            if (IsNull(archivoDto)
                ||IsEmpty(archivoDto!.Nombre)
                || IsNull(archivoDto.Archivo)
                || archivoDto.Archivo!.Length <= 0)
            {
                return true;
            }
            return false;
        }

//////////////////////
        private bool IsEmpty(string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

//////////////////////
        private bool IsNull(object? objeto)
        {
            if (objeto == null)
            {
                return true;
            }
            return false;
        }
    }
}
