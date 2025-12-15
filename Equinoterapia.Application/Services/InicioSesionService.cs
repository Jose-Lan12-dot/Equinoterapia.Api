using Equinoterapia.Application.DTOs.Auth;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services 
{
    public class InicioSesionService : IInicioSesionService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public InicioSesionService(
            IAuthRepository authRepository,
            IEncryptionService encryptionService,
            IJwtTokenGenerator tokenGenerator)
        {
            _authRepository = authRepository;
            _encryptionService = encryptionService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<(bool, string, LoginResponseDto?)> LoginAsync(CredencialesDto inicioSesionDataDto)
        {
            var usuario = await _authRepository.ValidateUserAsync(inicioSesionDataDto.Usuario);
            if (IsNull(usuario) || IsNull(usuario.IdUsuario) || usuario.IdUsuario == 0)
                return (false, "Usuario no encontrado", null);

            //string encryptedPassword = _encryptionService.EncryptPassword(inicioSesionDataDto.contrasena);
            string encryptedPassword = inicioSesionDataDto.contrasena; 

            var acceso = await _authRepository.ValidateAccessAsync(
                                    inicioSesionDataDto.Usuario,
                                    encryptedPassword,
                                    usuario.IdUsuario);

            if (IsNull(acceso) || acceso.IdUsuario == 0)
                return (false, "Credenciales incorrectas", null);


            var accesoDto = MapToDto(acceso);
            var token = _tokenGenerator.GenerateToken(accesoDto);
            var respuesta = new LoginResponseDto
            {
                Token = token,
                NombreUsuario = usuario.Usuario, // O usuario.Nombre si tienes esa propiedad
                FotoPerfil = usuario.Foto,       // Aquí pasas los bytes de la imagen
                Mensaje = "Inicio de sesión exitoso"
            };

            return (true, "Usuario encontrado", respuesta);
        }





        // --- MÉTODOS PRIVADOS (Helper) ---
        private bool IsNull(object? obj) { 
            if (obj == null) return true;
            return false;
        }

        private GetDataUsuarioDto MapToDto(InicioSesion usuario)
        {
            return new GetDataUsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                IdKronos = usuario.IdKronos,
                Usuario = usuario.Usuario,
                NombreCompleto = usuario.NombreCompleto,
                Mensaje = usuario.Mensaje,
                Foto = usuario.Foto,
                IdPerfil = usuario.IdPerfil,
                Perfil = usuario.Perfil
            };
        }
    }
}