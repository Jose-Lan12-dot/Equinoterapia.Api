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

        // Obtener todos los caballos
        public async Task<List<CaballoDto>> GetAllAsync()
        {
            // Llamamos a la ruta real del backend: /api/caballos
            var response = await _http.GetFromJsonAsync<List<CaballoDto>>("api/caballos");
            return response ?? new List<CaballoDto>();
        }

        // Obtener por ID
        public async Task<CaballoDto?> GetByIdAsync(int id)
        {
            // Llamamos a la ruta real del backend: /api/caballos/{id}
            var response = await _http.GetFromJsonAsync<CaballoDto>($"api/caballos/{id}");
            return response;
        }

        // Agregar nuevo caballo
        public async Task<bool> AddAsync(AddCaballoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/caballos", dto);
            return response.IsSuccessStatusCode;
        }

        // Actualizar parcialmente
        public async Task<bool> UpdatePartialAsync(int id, CaballoDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/caballos/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // Eliminar caballo
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/caballos/{id}");
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
