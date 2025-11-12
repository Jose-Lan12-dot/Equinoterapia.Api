using Equinoterapia.Application.DTOs.Auth;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInicioSesionService
    {
        Task<(bool, string, string)> LoginAsync(InicioSesionDataDto inicioSesionDataDto);
    }
}
