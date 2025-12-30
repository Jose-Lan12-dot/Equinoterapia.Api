using Blazored.LocalStorage;
using FRONTproyectoEquinoterapia.Models;
using FRONTproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
public class UsuarioSesionService
{
    public UsuarioSesionDto? Usuario { get; private set; }
    public event Action? OnChange;

    public void SetUsuario(UsuarioSesionDto usuario)
    {
        Usuario = usuario;
        OnChange?.Invoke();
    }

    public void LimpiarUsuario()
    {
        Usuario = null;
        OnChange?.Invoke();
    }

    public async Task CargarDesdeLocalStorageAsync(ILocalStorageService localStorage)
    {
        var nombre = await localStorage.GetItemAsync<string>("usuarioNombre");
        var foto = await localStorage.GetItemAsync<string>("usuarioFoto");

        if (!string.IsNullOrEmpty(nombre))
        {
            Usuario = new UsuarioSesionDto
            {
                Nombre = nombre,
                FotoBase64 = foto
            };

            OnChange?.Invoke();
        }
    }
}

