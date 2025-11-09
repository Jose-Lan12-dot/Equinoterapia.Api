using Equinoterapia.Application.DTOs;
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

        public async Task<(bool, string, string)> LoginAsync(InicioSesionDataDto inicioSesionDataDto)
        {
            if (ValidateData(inicioSesionDataDto))
                return (false, "Datos de usuario incompletos", "");

            // 1. Validar Usuario (Acción 1)
            var usuario = await _authRepository.ValidateUserAsync(inicioSesionDataDto.Usuario);
            if (IsNull(usuario) || IsNull(usuario.IdUsuario) || usuario.IdUsuario == 0)
                return (false, "Usuario no encontrado", "");

            // 2. Cifrar Contraseña (cuando esté listo)
            // string encryptedPassword = _encryptionService.EncryptPassword(inicioSesionDataDto.contrasena);
            string encryptedPassword = inicioSesionDataDto.contrasena; 

            // 3. Validar Acceso (Acción 2)
            var acceso = await _authRepository.ValidateAccessAsync(
                                    inicioSesionDataDto.Usuario,
                                    encryptedPassword,
                                    usuario.IdUsuario);

            if (IsNull(acceso) || acceso.IdUsuario == 0)
                return (false, "Credenciales incorrectas", "");


            var accesoDto = MapToDto(acceso);
            var token = _tokenGenerator.GenerateToken(accesoDto);

            return (true, "Usuario encontrado", token);
        }


        // --- MÉTODOS PRIVADOS (Helper) ---

        private bool IsEmpty(string? str) => string.IsNullOrWhiteSpace(str);
        private bool IsNull(object? obj) => obj is null;

        private bool ValidateData(InicioSesionDataDto inicioSesionDataDto)
        {
            if (IsEmpty(inicioSesionDataDto.Usuario) || IsEmpty(inicioSesionDataDto.contrasena))
                return true;
            return false;
        }

        private InicioSesionDto MapToDto(InicioSesion usuario)
        {
            return new InicioSesionDto
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