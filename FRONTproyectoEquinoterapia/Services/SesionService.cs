using Radzen;
using FRONTproyectoEquinoterapia.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Net.Http.Json;


namespace FRONTproyectoEquinoterapia.Services
{
    public class SesionesService
    {
        private readonly HttpClient _http;

        public SesionesService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GetAllSesionDto>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<List<GetAllSesionDto>>("api/sesiones");
            return res ?? new List<GetAllSesionDto>();
        }

        public async Task<List<GetAllSesionDto>> GetByWeekAsync(DateTime startOfWeek)
        {
            var dateStr = startOfWeek.ToString("yyyy-MM-dd");
            var res = await _http.GetFromJsonAsync<List<GetAllSesionDto>>($"api/sesiones/by-week?startOfWeek={dateStr}");
            return res ?? new List<GetAllSesionDto>();
        }

        public async Task<GetOneSesionDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<GetOneSesionDto>($"api/sesiones/{id}");
        }

        public async Task<FormDataDto?> GetFormDataAsync()
        {
            return await _http.GetFromJsonAsync<FormDataDto>("api/sesiones/form-data");
        }

        public async Task<List<HorarioOcupadoDto>> GetHorariosOcupadosAsync(DateTime fecha)
        {
            var dateStr = fecha.ToString("yyyy-MM-dd");
            var res = await _http.GetFromJsonAsync<List<HorarioOcupadoDto>>($"api/sesiones/horarios-ocupados?fecha={dateStr}");
            return res ?? new List<HorarioOcupadoDto>();
        }

        public async Task<(bool ok, string mensaje)> CreateAsync(AddSesionDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/sesiones", dto);

            if (!res.IsSuccessStatusCode)
            {
                // Leer como Dictionary<string, object>
                var err = await res.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                return (false, err != null && err.ContainsKey("Mensaje") ? err["Mensaje"].ToString()! : "Error");
            }

            // Para cuando es exitoso
            var okObj = await res.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            return (true, okObj != null && okObj.ContainsKey("Mensaje") ? okObj["Mensaje"].ToString()! : "Creado");
        }


        public async Task<bool> UpdateAsync(int id, UpdateSesionDto dto)
        {
            var res = await _http.PatchAsync($"api/sesiones/{id}", JsonContent.Create(dto));
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/sesiones/{id}");
            return res.IsSuccessStatusCode;
        }
    }

}
