using Equinoterapia.Application.DTOs.Auth;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInicioSesionService
    {
        Task<(bool, string, LoginResponseDto?)> LoginAsync(CredencialesDto inicioSesionDataDto);
        Task<(bool, string?, byte[]?)> ObtenerPerfilAsync(CredencialesDto inicioSesionDataDto);
    }
}
