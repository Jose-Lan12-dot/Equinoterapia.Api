using Radzen;
using FRONTproyectoEquinoterapia.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FRONTproyectoEquinoterapia.Services
{
    public class SesionService : ISesionService
    {
        private readonly HttpClient _http;

        private const string ApiBase = "api/sesiones";

        public SesionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<(IEnumerable<SesionDto>, string)> ObtenerSesionesPorSemanaAsync(DateOnly startOfWeek)
        {
            string fechaStr = startOfWeek.ToString("yyyy-MM-dd");
            var response = await _http.GetAsync($"{ApiBase}/by-week?startOfWeek={fechaStr}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<SesionDto>>();
                return (data ?? new List<SesionDto>(), "Datos cargados");
            }
            var mensaje = await response.Content.ReadAsStringAsync();
            return (new List<SesionDto>(), mensaje);
        }

        public async Task<(SesionDto?, string)> ObtenerSesionPorIdAsync(int sesionId) // <-- implementado
        {
            var response = await _http.GetAsync($"{ApiBase}/{sesionId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<SesionDto>();
                return (data, "Sesión cargada");
            }
            var mensaje = await response.Content.ReadAsStringAsync();
            return (null, mensaje);
        }

        public async Task<(bool, string)> AddAsync(AddSesionDto nuevaSesion)
        {
            var response = await _http.PostAsJsonAsync(ApiBase, nuevaSesion);
            var mensaje = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, mensaje);
        }

        public async Task<(bool, string)> UpdateAsync(int sesionId, SesionDto sesionDto)
        {
            var response = await _http.PutAsJsonAsync($"{ApiBase}/{sesionId}", sesionDto);
            var mensaje = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, mensaje);
        }

        public async Task<(bool, string)> DeleteAsync(int sesionId)
        {
            var response = await _http.DeleteAsync($"{ApiBase}/{sesionId}");
            var mensaje = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, mensaje);
        }

        public async Task<bool> EliminarSesionAsync(int sesionId, IJSRuntime js) // <-- compatibilidad con diálogo
        {
            var (exito, mensaje) = await DeleteAsync(sesionId);
            if (!exito)
                await js.InvokeVoidAsync("alert", mensaje);
            return exito;
        }

        public async Task<bool> AgregarPacienteAsync(int sesionId, int pacienteId) // <-- para agregar pacientes
        {
            var response = await _http.PostAsJsonAsync($"{ApiBase}/{sesionId}/pacientes", new { PacienteId = pacienteId });
            return response.IsSuccessStatusCode;
        }

        public async Task<(IEnumerable<SesionDto>, string)> ObtenerSesionesAsync()
        {
            var response = await _http.GetAsync($"{ApiBase}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<SesionDto>>();
                return (data ?? new List<SesionDto>(), "Sesiones cargadas");
            }

            var mensaje = await response.Content.ReadAsStringAsync();
            return (new List<SesionDto>(), mensaje);
        }

        public async Task<List<InstructorDto>> ObtenerTodosInstructoresAsync()
        {
            return await _http.GetFromJsonAsync<List<InstructorDto>>("api/instructores") ?? new List<InstructorDto>();
        }

        public async Task<List<CaballoDto>> ObtenerTodosCaballosAsync()
        {
            return await _http.GetFromJsonAsync<List<CaballoDto>>("api/caballos") ?? new List<CaballoDto>();
        }

    }
}
