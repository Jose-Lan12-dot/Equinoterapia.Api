using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FRONTproyectoEquinoterapia.Models;

namespace FRONTproyectoEquinoterapia.Services //simulacion mientras no tengamos backend
{
    public class LoginService
    {
        // Método simulado que reemplaza la llamada real a la API
        public async Task<bool> LoginAsync(LoginModel login)
        {
            await Task.Delay(500); // Simula tiempo de respuesta

            // 🔹 Simulación: usuario correcto = "admin", contraseña = "1234"
            if (login.Usuario == "admin" && login.Contrasena == "1234")
            {
                return true;
            }
            return false;
        }
    }
}

/*
namespace Equinoterapia2.Services
{
    public class LoginService
    {
        private readonly HttpClient _http;

        public LoginService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> LoginAsync(LoginModel login)
        {
            // Reemplaza esta URL con la de tu API real
            var response = await _http.PostAsJsonAsync("https://localhost:5001/api/auth/login", login);
            return response.IsSuccessStatusCode;
        }
    }
}
*/
