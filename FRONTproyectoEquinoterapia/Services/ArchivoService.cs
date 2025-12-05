using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FRONTproyectoEquinoterapia.Services
{
    public class ArchivoService
    {
        private readonly HttpClient _http;

        public ArchivoService(HttpClient http)
        {
            _http = http;
        }

        // ===========================
        // Subir archivo
        // ===========================
        public async Task<(bool exito, string mensaje)> SubirArchivoAsync(int pacienteId, AddArchivoBlazorDto archivo)
        {
            using var formData = new MultipartFormDataContent();

            // Enviar solo lo que el backend pide: Nombre + Archivo
            formData.Add(new StringContent(archivo.Nombre ?? ""), "Nombre");

            var streamContent = new StreamContent(archivo.Archivo.OpenReadStream(10 * 1024 * 1024));
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.Archivo.ContentType);
            formData.Add(streamContent, "Archivo", archivo.Archivo.Name);

            var response = await _http.PostAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos", formData);
            if (response.IsSuccessStatusCode)
                return (true, "Archivo subido correctamente");

            var error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }



        // ===========================
        // Actualizar archivo
        // ===========================
        public async Task<(bool exito, string mensaje)> ActualizarArchivoAsync(int pacienteId, UpdateArchivoBlazorDto archivo)
        {
            using var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(archivo.Nombre ?? ""), "Nombre");

            var streamContent = new StreamContent(archivo.Archivo.OpenReadStream(10 * 1024 * 1024));
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.Archivo.ContentType);
            formData.Add(streamContent, "Archivo", archivo.Archivo.Name);

            var response = await _http.PutAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos", formData);

            if (response.IsSuccessStatusCode)
                return (true, "Archivo actualizado correctamente");

            var error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

        // ===========================
        // Eliminar archivo
        // ===========================
        public async Task<(bool exito, string mensaje)> EliminarArchivoAsync(int pacienteId, string nombreArchivo)
        {
            var response = await _http.DeleteAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos/{nombreArchivo}");

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
            var response = await _http.GetAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos/{nombreArchivo}");

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
            var response = await _http.GetFromJsonAsync<List<TipoArchivoDto>>("https://localhost:7280/obtener-tipoarchivo");
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
