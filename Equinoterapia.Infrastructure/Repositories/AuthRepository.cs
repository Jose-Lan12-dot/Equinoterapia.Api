using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InicioSesion?> ValidateUserAsync(string usuario)
        {
            var resultados = await _context.inicioSesion
                .FromSqlInterpolated($@"
                    EXEC dbo.Sp_LoginNovedades 
                        @User = {usuario}, 
                        @Password = '', 
                        @IdSistema = {2130}, 
                        @IdPerfil = {0}, 
                        @Accion = {1}")
                .ToListAsync();

            return resultados.SingleOrDefault();
        }

        public async Task<InicioSesion?> ValidateAccessAsync(string usuario, string password, int idUsuario)
        {
            var resultados = await _context.inicioSesion
                .FromSqlInterpolated($@"
                    EXEC dbo.Sp_LoginNovedades 
                        @User = {usuario}, 
                        @Password = {password}, 
                        @IdSistema = {2130}, 
                        @IdPerfil = {idUsuario}, 
                        @Accion = {2}")
                .ToListAsync();

            return resultados.SingleOrDefault();
        }
    }
}