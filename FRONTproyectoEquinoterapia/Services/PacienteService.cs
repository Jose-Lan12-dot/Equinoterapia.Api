using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
public class PacienteService
{
    private readonly HttpClient _http;

    public PacienteService(HttpClient http)
    {
        _http = http;
    }

    // ==========================
    // PACIENTES (ya existentes)
    // ==========================
    public async Task<List<PacienteDto>> ObtenerTodosAsync()
    {
        var response = await _http.GetFromJsonAsync<ResponseWrapper<List<PacienteDto>>>("https://localhost:7280/api/Pacientes");
        return response?.Data ?? new List<PacienteDto>();
    }

    public async Task<PacienteDetalleResponse?> ObtenerPorIdAsync(int pacienteId)
    {
        var response = await _http.GetFromJsonAsync<ResponseWrapper<PacienteDto>>($"https://localhost:7280/api/Pacientes/{pacienteId}");
        if (response == null) return null;

        return new PacienteDetalleResponse
        {
            Paciente = response.Data,
            Mensaje = response.Message
        };
    }

    public async Task<(bool exito, string mensaje)> AgregarPacienteAsync(AddPacienteConArchivosDto dto)
    {
        using var formData = new MultipartFormDataContent();

        formData.Add(new StringContent(dto.Paciente.Nombre ?? ""), "Paciente.Nombre");
        formData.Add(new StringContent(dto.Paciente.Tutor ?? ""), "Paciente.Tutor");
        formData.Add(new StringContent(dto.Paciente.TelefonoTutor ?? ""), "Paciente.TelefonoTutor");
        formData.Add(new StringContent(dto.Paciente.Genero ?? ""), "Paciente.Genero");
        formData.Add(new StringContent(dto.Paciente.FechaNacimiento.ToString("yyyy-MM-dd")), "Paciente.FechaNacimiento");

        // No se envían archivos al crear paciente, los subimos después
        var response = await _http.PostAsync("https://localhost:7280/api/Pacientes", formData);
        if (response.IsSuccessStatusCode)
            return (true, "Paciente agregado correctamente");

        string error = await response.Content.ReadAsStringAsync();
        return (false, $"Error: {response.StatusCode} - {error}");
    }

    public async Task<bool> ActualizarParcialAsync(UpdatePacienteDto paciente)
    {
        var response = await _http.PatchAsJsonAsync($"https://localhost:7280/api/Pacientes/{paciente.PacienteId}", paciente);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarAsync(int pacienteId)
    {
        var response = await _http.DeleteAsync($"https://localhost:7280/api/Pacientes/{pacienteId}");
        return response.IsSuccessStatusCode;
    }

    // ==========================
    // ARCHIVOS
    // ==========================

    // Subir archivo
    public async Task<(bool exito, string mensaje)> SubirArchivoAsync(int pacienteId, AddArchivoBlazorDto archivoDto)
    {
        using var formData = new MultipartFormDataContent();

        formData.Add(new StringContent(archivoDto.Nombre ?? ""), "Nombre");

        var streamContent = new StreamContent(archivoDto.Archivo.OpenReadStream());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivoDto.Archivo.ContentType);

        formData.Add(streamContent, "Archivo", archivoDto.Archivo.Name);

        var response = await _http.PostAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos", formData);

        if (response.IsSuccessStatusCode)
            return (true, "Archivo subido correctamente");

        string error = await response.Content.ReadAsStringAsync();
        return (false, $"Error: {response.StatusCode} - {error}");
    }

    // Descargar archivo
    public async Task<byte[]?> ObtenerArchivoAsync(int pacienteId, string nombreArchivo)
    {
        var response = await _http.GetAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos/{nombreArchivo}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadAsByteArrayAsync();
    }

    // Actualizar archivo
    public async Task<(bool exito, string mensaje)> ActualizarArchivoAsync(int pacienteId, UpdateArchivoBlazorDto archivoDto)
    {
        using var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(archivoDto.Nombre ?? ""), "Nombre");

        if (archivoDto.Archivo != null)
        {
            var streamContent = new StreamContent(archivoDto.Archivo.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivoDto.Archivo.ContentType);
            formData.Add(streamContent, "Archivo", archivoDto.Archivo.Name);
        }

        var response = await _http.PutAsync($"https://localhost:7280/api/pacientes/{pacienteId}/archivos", formData);

        if (response.IsSuccessStatusCode)
            return (true, "Archivo actualizado correctamente");

        string error = await response.Content.ReadAsStringAsync();
        return (false, $"Error: {response.StatusCode} - {error}");
    }
}

// Wrapper para respuestas
public class ResponseWrapper<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}
