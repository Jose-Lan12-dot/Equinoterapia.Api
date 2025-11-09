using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInicioSesionService
    {
        Task<(bool, string, string)> LoginAsync(InicioSesionDataDto inicioSesionDataDto);
    }
}
