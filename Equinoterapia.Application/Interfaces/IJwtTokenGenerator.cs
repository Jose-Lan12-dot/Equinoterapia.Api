using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(InicioSesionDto inicioSesionDto);
    }
}