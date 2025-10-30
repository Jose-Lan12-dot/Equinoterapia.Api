using FrontEquinoterapia.Data.Models;
using FRONTproyectoEquinoterapia.Dtos;
using System.Net.Http.Json;

namespace FrontEquinoterapia.Data.Services
{
    public class InicioSesionService
    {
        private readonly HttpClient _httpClient;

        public InicioSesionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string,bool,string)> AccesoAsync(InicioSesionModel credenciales)
        {
            var respuesta  = await _httpClient.PostAsJsonAsync("/api/InicioSesion/acceso", credenciales);
            if (respuesta.IsSuccessStatusCode)
            {
                var datos = await respuesta.Content.ReadFromJsonAsync<RespuestaLoginDto>();
                return (datos?.mensaje ?? "", true, datos?.token ?? "");
            }
            else
            {
                var datos = await respuesta.Content.ReadFromJsonAsync<RespuestaLoginDto>();
                return (datos?.mensaje ?? "Error de autenticación", false, "");
            }
        }
    }
}
