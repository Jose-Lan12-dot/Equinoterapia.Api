using System.Net.Http.Json;
using FRONTproyectoEquinoterapia.Models;

namespace FRONTproyectoEquinoterapia.Services
{
    public class InstructorService
    {
        private readonly HttpClient _http;

        public InstructorService(HttpClient http)
        {
            _http = http;
        }

        private class InstructorResponse
        {
            public string Mensaje { get; set; } = "";
            public List<InstructorDto> Instructores { get; set; } = new();
        }

        public async Task<List<InstructorDto>> GetAllAsync()
        {
            // Llama a la ruta correcta
            var response = await _http.GetFromJsonAsync<List<InstructorDto>>("api/instructores");
            return response ?? new List<InstructorDto>();
        }

        public async Task<InstructorDto?> GetByIdAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<InstructorDto>($"api/instructores/{id}");
            return result;
        }

        public async Task<bool> AddAsync(AddInstructorDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/instructores", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePartialAsync(int id, InstructorDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/instructores/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/instructores/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
