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
            var response = await _http.GetFromJsonAsync<InstructorResponse>("api/instructor/obtener-todos");
            return response?.Instructores ?? new List<InstructorDto>();
        }

        public async Task<InstructorDto?> GetByIdAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<InstructorResponse>($"api/instructor/obtener-por-id/{id}");
            return result?.Instructores.FirstOrDefault();
        }

        public async Task<bool> AddAsync(AddInstructorDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/instructor/agregar", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAllAsync(InstructorDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/instructor/actualizar-todo/{dto.InstructorId}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePartialAsync(int id, InstructorDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/instructor/actualizar-parcialmente/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/instructor/eliminar/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
