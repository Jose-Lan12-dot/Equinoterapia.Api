using System.Net.Http;
using System.Net.Http.Json;
using FRONTproyectoEquinoterapia.Models;
using FRONTproyectoEquinoterapia.Services;
using System.Threading.Tasks;

namespace FRONTproyectoEquinoterapia.Services
{
    public class InformeService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public InformeService(HttpClient httpClient, ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        private async Task AddAuthHeaderAsync()
        {
            var token = await _tokenProvider.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<InformeDto?> GetInformeAsync(int mes, int anio)
        {
            await AddAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"api/Informes?mes={mes}&anio={anio}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<InformeDto>();
            }
            return null;
        }

        public async Task<GetEstadisticasDto?> GetEstadisticasAsync(int mes, int anio)
        {
            await AddAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"api/Informes/estadisticas?mes={mes}&anio={anio}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetEstadisticasDto>();
            }
            return null;
        }

        public async Task<(bool exito, string mensaje)> GuardarInformeAsync(InformeDto informe)
        {
            await AddAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Informes", informe);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                return (true, result?.Mensaje ?? "Guardado correctamente.");
            }
            else
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                return (false, result?.Mensaje ?? "Error al guardar.");
            }
        }
    }

    // Clase auxiliar para leer mensajes del backend
    public class ResponseMessage
    {
        public string Mensaje { get; set; } = string.Empty;
    }
}
