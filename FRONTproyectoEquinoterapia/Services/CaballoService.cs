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

        // ✅ Obtener todos los caballos (ajustado al formato real del backend)
        public async Task<List<CaballoDto>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<CaballosResponse>("api/caballo/obtener/todos");
            return response?.Caballos ?? new List<CaballoDto>();
        }

        // ✅ Obtener por ID
        public async Task<CaballoDto?> GetByIdAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<CaballoResponse>($"api/caballo/obtener/por/{id}");
            return response?.Caballo;
        }

        // 🟢 Agregar nuevo caballo
        public async Task<bool> AddAsync(AddCaballoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/caballo/agregar", dto);
            return response.IsSuccessStatusCode;
        }

        // 🟠 Actualizar parcialmente
        public async Task<bool> UpdatePartialAsync(int id, CaballoDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/caballo/actualizar-parcialmente/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // 🔴 Eliminar caballo
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/caballo/eliminar/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // ✅ Clases auxiliares que reflejan la respuesta del backend
    public class CaballosResponse
    {
        public string Mensaje { get; set; } = string.Empty;
        public List<CaballoDto> Caballos { get; set; } = new();
    }

    public class CaballoResponse
    {
        public string Mensaje { get; set; } = string.Empty;
        public CaballoDto? Caballo { get; set; }
    }
}
