using FRONTproyectoEquinoterapia.Models;
using FRONTproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;

public class UsuarioSesionService
{
    private readonly HttpClient _http;

    public UsuarioSesionDto? Usuario { get; set; }
    public event Action? OnChange;

    public UsuarioSesionService(HttpClient http)
    {
        _http = http;
    }

    public void NotifyStateChanged() => OnChange?.Invoke();

    public async Task CargarPerfilAsync()
    {
        Usuario = await _http.GetFromJsonAsync<UsuarioSesionDto>(
            "api/usuarios/perfil"
        );

        NotifyStateChanged();
    }

    public void LimpiarSesion()
    {
        Usuario = null;
        NotifyStateChanged();
    }
}
