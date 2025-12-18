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

            if (string.IsNullOrWhiteSpace(token))
            {
                return Anonymous();
            }

            JwtSecurityToken? jwt = null;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                jwt = handler.ReadJwtToken(token);
            }
            catch
            {
                await LogoutAsync();
                return Anonymous();
            }

            // Token vencido
            if (jwt.ValidTo < DateTime.UtcNow)
            {
                await LogoutAsync();
                return Anonymous();
            }

            // Creamos Claims del JWT
            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
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
            var token = await _localStorage.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var nombre = jwt.Claims.FirstOrDefault(c => c.Type == "nombre")?.Value;
            var foto = jwt.Claims.FirstOrDefault(c => c.Type == "foto")?.Value;

            return new UsuarioSesionDto
            {
                Nombre = nombre ?? "",
                FotoBase64 = foto
            };
        }


    }
}
