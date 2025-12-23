using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FRONTproyectoEquinoterapia.Services
{
    public class ArchivoService
    {
        private readonly HttpClient _http;

        public ArchivoService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API"); // ✅ ESTE SÍ LLEVA TOKEN
        }
        

        // ===========================
        // Subir archivo
        // ===========================
        public async Task<(bool exito, string mensaje)> SubirArchivoAsync(int pacienteId, IBrowserFile file, string nombreArchivo)
        {
            try
            {
                // Validar tamaño máximo 20 MB
                if (file.Size > 20_000_000)
                    return (false, "El archivo es demasiado grande. Máximo permitido: 20 MB.");

                using var stream = file.OpenReadStream(maxAllowedSize: 20_000_000);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                var dto = new AddArchivoBlazorDto
                {
                    Nombre = nombreArchivo,
                    Archivo = ms.ToArray()
                };

                var response = await _http.PostAsJsonAsync($"api/pacientes/{pacienteId}/archivos", dto);

                if (!response.IsSuccessStatusCode)
                    return (false, "Error al subir el archivo.");

                return (true, "Archivo subido correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool exito, string mensaje)> ActualizarArchivoAsync(int pacienteId, IBrowserFile file, string nombreArchivo)
        {
            try
            {
                if (file.Size > 20_000_000)
                    return (false, "El archivo es demasiado grande. Máximo permitido: 20 MB.");

                using var stream = file.OpenReadStream(maxAllowedSize: 20_000_000);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                var dto = new UpdateArchivoBlazorDto
                {
                    Nombre = nombreArchivo,
                    Archivo = ms.ToArray()
                };

                var response = await _http.PutAsJsonAsync($"api/pacientes/{pacienteId}/archivos", dto);

                if (!response.IsSuccessStatusCode)
                    return (false, "Error al actualizar el archivo.");

                return (true, "Archivo actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }



        // ===========================
        // Eliminar archivo
        // ===========================
        public async Task<(bool exito, string mensaje)> EliminarArchivoAsync(int pacienteId, string nombreArchivo)
        {
            var response = await _http.DeleteAsync($"/api/pacientes/{pacienteId}/archivos/{nombreArchivo}");

            if (response.IsSuccessStatusCode)
                return (true, "Archivo eliminado correctamente");

            var error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

        // ===========================
        // Descargar archivo
        // ===========================
        public async Task<(byte[]? contenido, string mensaje, string extension)> DescargarArchivoAsync(int pacienteId, string nombreArchivo)
        {
            var response = await _http.GetAsync($"api/pacientes/{pacienteId}/archivos/{nombreArchivo}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (null, $"Error: {response.StatusCode} - {error}", "");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();

            // Intentamos sacar extensión del Content-Disposition
            var extension = System.IO.Path.GetExtension(nombreArchivo);
            return (bytes, "Archivo descargado correctamente", extension);
        }

        // Obtener tipos de archivo desde backend
        public async Task<List<TipoArchivoDto>> ObtenerTiposAsync()
        {
            var response = await _http.GetFromJsonAsync<List<TipoArchivoDto>>("api/archivos/obtener-tipoarchivo");
            return response ?? new List<TipoArchivoDto>();
        }



        // DTO para Blazor
        public class TipoArchivoDto
        {
            public int TipoId { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Extension { get; set; } = string.Empty;
        }



    }
}
