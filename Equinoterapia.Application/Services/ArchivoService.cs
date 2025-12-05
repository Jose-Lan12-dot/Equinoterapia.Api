using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Equinoterapia.Application.DTOs.Archivos;

namespace Equinoterapia.Application.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;
        private readonly IArchivoQueryRepository _archivoQueryRepository;

        public ArchivoService(IArchivoRepository archivoRepository, IArchivoQueryRepository archivoQueryRepository)
        {
            _archivoRepository = archivoRepository;
            _archivoQueryRepository = archivoQueryRepository;
        }


        public async Task<(GetByIdArchivoDto?, string)> GetAsync(string nombreArchivo, int pacienteId)
        {
            var tipoArchivo = await _archivoRepository.GetTipoArchivoPorNombreAsync(nombreArchivo);
            if (IsNull(tipoArchivo))
            {
                return (null, "Tipo de archivo no encontrado");
            }

            var archivo = await _archivoRepository.GetArchivoAsync(pacienteId, tipoArchivo!.TipoId);
            if (IsNull(archivo))
            {
                return (null, "Archivo no encontrado");
            }


            return (
                new GetByIdArchivoDto
                {
                    Archivo = archivo.Archivo,
                    Extension = tipoArchivo.Extension
                }, "Archivos encontrado");
        }

        public async Task<(bool, string)> AddAsync(AddArchivoDto archivos, int pacienteId)
        {

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

            var nuevoArchivo = new Archivos
            {
                PacienteId = pacienteId,
                TipoId = tipoId,
                Archivo = archivos.Archivo!
            };

            try
            {
                _archivoRepository.Add(nuevoArchivo);
                await _archivoRepository.SaveChangesAsync();
                return (true, archivos.Nombre + " Agregado correctamnete");
            }
            catch (Exception ex)
            {
                return (false, "Error al agregar el archivo: " + ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(UpdateArchivoDto archivoDto, int pacienteId)
        {

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
                archivo.Archivo = archivoDto.Archivo;

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

       
        public async Task<List<GetTipoArchivoDto>> GetTipoArchivo()
        {
            return await _archivoQueryRepository.GetTiposArchivos();

        }







        // --- MÉTODOS PRIVADOS (Helper) ---

        private async Task<byte[]> FileToArrayAsync(IFormFile archivo)
        {
            using var ms = new MemoryStream();
            await archivo.CopyToAsync(ms);
            return ms.ToArray();
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

