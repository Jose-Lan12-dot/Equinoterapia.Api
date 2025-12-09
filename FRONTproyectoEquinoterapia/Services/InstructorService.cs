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

        public async Task<List<InstructorDto>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<List<InstructorDto>>("api/instructores");
            return response ?? new List<InstructorDto>();
        }

        public async Task<InstructorDto?> GetByIdAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<InstructorDto>($"api/instructores/{id}");
            return response;
        }

        public async Task<bool> AddAsync(AddInstructorDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/instructores", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePartialAsync(int id, UpdateInstructorDto dto)
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
