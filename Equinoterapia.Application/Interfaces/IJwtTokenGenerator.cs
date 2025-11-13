using Equinoterapia.Application.DTOs.Auth;

namespace Equinoterapia.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(GetDataUsuarioDto inicioSesionDto);
    }
}