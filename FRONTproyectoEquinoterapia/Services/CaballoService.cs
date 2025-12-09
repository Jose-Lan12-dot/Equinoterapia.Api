using System.Net.Http.Json;
using FRONTproyectoEquinoterapia.Models;

namespace FRONTproyectoEquinoterapia.Services
{
    public class CaballoService
    {
        private readonly HttpClient _http;

        public CaballoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CaballoDto>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<List<CaballoDto>>("api/caballos");
            return response ?? new List<CaballoDto>();
        }

        public async Task<CaballoDto?> GetByIdAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<CaballoDto>($"api/caballos/{id}");
            return response;
        }

        public async Task<bool> AddAsync(AddCaballoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/caballos", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePartialAsync(int id, UpdateCaballoDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/caballos/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/caballos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
