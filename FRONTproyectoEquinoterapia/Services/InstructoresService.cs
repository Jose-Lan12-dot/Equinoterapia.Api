using FrontEquinoterapia.Data.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrontEquinoterapia.Data.Models;

namespace FrontEquinoterapia.Data.Services
{
    public class InstructoresService
    {

        private readonly HttpClient _httpClient;

        public InstructoresService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Modelo auxiliar para coincidir con la estructura del JSON
        private class InstructoresResponse
        {
            public string mensaje { get; set; }
            public List<InstructorModel> instructores { get; set; }
        }

        public async Task<List<InstructorModel>> GetInstructoresAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<InstructoresResponse>("api/Instructor/obtener");

                if (response != null && response.instructores != null)
                    return response.instructores;

                return new List<InstructorModel>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error HTTP al obtener instructores: {ex.Message}");
                return new List<InstructorModel>();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error general al obtener instructores: {ex.Message}");
                return new List<InstructorModel>();
            }
        }
    }
}






