using Equinoterapia.Domain.Entities;

namespace Equinoterapia.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<InicioSesion?> ValidateUserAsync(string usuario);

        Task<InicioSesion?> ValidateAccessAsync(string usuario, string password, int idUsuario);
    }
}