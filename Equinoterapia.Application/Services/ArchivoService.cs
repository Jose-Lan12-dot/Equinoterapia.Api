using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Http; 

namespace Equinoterapia.Application.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;

        public ArchivoService(IArchivoRepository archivoRepository)
        {
            _archivoRepository = archivoRepository;
        }

        public async Task<(byte[]?, string, string)> GetAsync(string nombreArchivo, int pacienteId)
        {
            // Usamos el repositorio
            var tipoArchivo = await _archivoRepository.GetTipoArchivoPorNombreAsync(nombreArchivo);
            if (IsNull(tipoArchivo))
            {
                return (null, "Tipo de archivo no encontrado", "");
            }

            var archivo = await _archivoRepository.GetArchivoAsync(pacienteId, tipoArchivo.TipoId);
            if (IsNull(archivo))
            {
                return (null, "Archivo no encontrado", "");
            }

            return (archivo.Archivo, "Archivos encontrado", tipoArchivo.Extension);
        }

        public async Task<(bool, string)> AddAsync(ArchivoDto archivos, int pacienteId)
        {
            // Lógica de negocio (Validación) se queda aquí
            if (ValidateFileData(archivos))
            {
                return (false, "Entrada no valida");
            }

            // Usamos el repositorio
            var tipoId = await _archivoRepository.GetTipoIdPorNombreAsync(archivos.Nombre!);
            if (tipoId == 0)
            {
                return (false, "Tipo de archivo no permitido");
            }

            if (!await _archivoRepository.PacienteExistsAsync(pacienteId))
            {
                return (false, "El paciente no existe");
            }

            if (await _archivoRepository.ArchivoExistsAsync(tipoId, pacienteId))
            {
                return (false, "Archivo del paciente ya existe");
            }

            // Lógica de negocio (Mapeo y Conversión) se queda aquí
            var nuevoArchivo = new Archivos
            {
                PacienteId = pacienteId,
                TipoId = tipoId,
                Archivo = await FileToArrayAsync(archivos.Archivo!)
            };

            try
            {
                // Usamos el repositorio
                _archivoRepository.Add(nuevoArchivo);
                await _archivoRepository.SaveChangesAsync();
                return (true, archivos.Nombre + " Agregado correctamnete");
            }
            catch (Exception ex)
            {
                return (false, "Error al agregar el archivo: " + ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(ArchivoDto archivoDto, int pacienteId)
        {
            if (ValidateFileData(archivoDto))
            {
                return (false, "Entrada no valida");
            }

            var tipoId = await _archivoRepository.GetTipoIdPorNombreAsync(archivoDto.Nombre!);
            if (tipoId == 0)
            {
                return (false, "Tipo de archivo no permitido");
            }

            var archivo = await _archivoRepository.GetArchivoAsync(pacienteId, tipoId);
            if (IsNull(archivo))
            {
                return (false, "Archivo del paciente no existe");
            }

            try
            {
                // Lógica de negocio
                archivo.Archivo = await FileToArrayAsync(archivoDto.Archivo!);

                // Usamos el repositorio
                await _archivoRepository.SaveChangesAsync();
                return (true, archivoDto.Nombre + " Actualizado Correctamnete");
            }
            catch (Exception ex)
            {
                return (false, "Error al actualizar el archivo: " + ex.Message);
            }
        }

        public async Task<(bool, string)> DeleteAsync(string nombreArchivo, int pacienteId)
        {
            var tipoId = await _archivoRepository.GetTipoIdPorNombreAsync(nombreArchivo);
            if (tipoId == 0)
            {
                return (false, "Tipo de archivo no permitido");
            }

            var archivo = await _archivoRepository.GetArchivoAsync(pacienteId, tipoId);
            if (IsNull(archivo))
            {
                return (false, "Archivo del paciente no existe");
            }

            try
            {
                _archivoRepository.Delete(archivo);
                await _archivoRepository.SaveChangesAsync();
                return (true, nombreArchivo + " eliminado correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el archivo: {ex.Message}");
            }
        }

       
        
        
        
        // --- MÉTODOS PRIVADOS (Helper) ---
        // ¡Toda esta lógica se queda en el servicio! Está perfecto aquí.

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
                || IsEmpty(archivoDto!.Nombre)
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

