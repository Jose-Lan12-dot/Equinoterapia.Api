using System.Net.Http.Json;

namespace FRONTproyectoEquinoterapia.Models
{
    public class InicioSesionService
    {
        private readonly HttpClient _httpClient;

        public InicioSesionService(IHttpClientFactory httpClientFactory)
        {
             _httpClient = httpClientFactory.CreateClient("PublicClient");
        }

        public async Task<(bool exito, string mensaje, RespuestaLoginDto? data)>
    AccesoAsync(InicioSesionModel credenciales)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Auth/login", credenciales);

            if (!response.IsSuccessStatusCode)
                return (false, "Credenciales incorrectas", null);

            var result =
                await response.Content.ReadFromJsonAsync<RespuestaLoginDto>();

            if (result == null || string.IsNullOrEmpty(result.Token))
                return (false, "Respuesta inválida del servidor", null);

            return (true, result.Mensaje, result);
        }




        /*public async Task<(bool exito, string mensaje, LoginDataDto? data)>
            AccesoAsync(InicioSesionModel credenciales)
        {

           // using var client = new HttpClient();
            //client.BaseAddress = new Uri("https://aplicaciones.utsistemas.gob.mx/apiequinoterapia/api/");
            //client.BaseAddress = new Uri("https://aplicaciones.utsistemas.gob.mx/apiequinoterapia/");
            //var response = await client.PostAsJsonAsync(
              //  "api/Auth/login", credenciales);

            var response = await _httpClient.PostAsJsonAsync(
              "api/Auth/login", credenciales);

            if (!response.IsSuccessStatusCode)
                return (false, "Credenciales incorrectas", null);

            var result =
                await response.Content.ReadFromJsonAsync<RespuestaLoginDto>();

            return (true, result!.Mensaje, result.Data);
        }*/
    }
}
