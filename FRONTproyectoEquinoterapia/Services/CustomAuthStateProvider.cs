using Blazored.LocalStorage;
using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FRONTproyectoEquinoterapia.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            Console.WriteLine($"[AUTH] Token existe: {!string.IsNullOrEmpty(token)}");

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("[AUTH] Token VACÍO");
                return Anonymous();
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Console.WriteLine($"[AUTH] Token válido hasta: {jwt.ValidTo}");

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }


        public async Task MarkUserAsAuthenticated(string token)
        {
            // Guardamos token en local storage
            await _localStorage.SetItemAsync("token", token);

            // Creamos Claims básicos (puedes personalizar)
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("token");
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous()));
        }

        private AuthenticationState Anonymous()
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task<UsuarioSesionDto?> ObtenerUsuarioDesdeToken()
        {
            Console.WriteLine("[JWT] Entró a ObtenerUsuarioDesdeToken");

            var token = await _localStorage.GetItemAsync<string>("token");
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var nombre = jwt.Claims.FirstOrDefault(c => c.Type == "nombre")?.Value;
            var foto = jwt.Claims.FirstOrDefault(c => c.Type == "foto")?.Value;

            Console.WriteLine($"[JWT] Nombre: {nombre}");
            Console.WriteLine($"[JWT] Foto null?: {string.IsNullOrEmpty(foto)}");

            return new UsuarioSesionDto
            {
                Nombre = nombre ?? "",
                FotoBase64 = string.IsNullOrEmpty(foto) ? null : foto
            };
        }


    }
}
