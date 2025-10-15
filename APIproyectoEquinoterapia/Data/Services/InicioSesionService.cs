using APIproyectoEquinoterapia.Data.Helpers;
using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.DTOs;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIproyectoEquinoterapia.Data.Services
{
    public class InicioSesionService
    {
        private readonly  AppDbContext _context;
        private readonly IConfiguration _configuration;

        public InicioSesionService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task <(bool,string,string)> PostInicioSesionAsync(DatosUsuarioDTO datosUsuarioDTO)
        {
            if (datosUsuarioDTO == null || string.IsNullOrEmpty(datosUsuarioDTO.Usuario) 
                || string.IsNullOrEmpty(datosUsuarioDTO.contrasena))
                return (false,"Datos de usuario incompletos","");


            //datosUsuarioDTO.contrasena = Encrypt.EncryptStringSspcWeb(datosUsuarioDTO.contrasena);

            var usuario = await ValidarUsuarioAsync(datosUsuarioDTO);
            if (usuario == null || usuario.IdUsuario == 0)
                return (false,"Usuario no encontrado","");

            var acceso = await ValidarAccesoAsync(datosUsuarioDTO, usuario.IdUsuario);
            if (acceso == null || acceso.IdUsuario == 0)
                return (false, "Credenciales incorrectas", "");

            //var acceso = await ValidarAccesoAsync(datosUsuarioDTO, usuario.IdUsuario);
            //if (acceso == null || acceso.IdUsuario == 0)
            //    return (false, "Credenciales incorrectas", "");

            var token = GenerarToken(datosUsuarioDTO, acceso.IdUsuario);

            return (true,"Usuario encontrado",token);
        }


        private async Task <InicioSesionDTO?> ValidarUsuarioAsync(DatosUsuarioDTO datosUsuarioDTO)
        {
            var resultados = await _context.inicioSesion
                .FromSqlInterpolated($@"
                    EXEC dbo.Sp_LoginNovedades 
                        @User = {datosUsuarioDTO.Usuario}, 
                        @Password = '', 
                        @IdSistema = {1}, 
                        @IdPerfil = {0}, 
                        @Accion = {1}")
                .ToListAsync();

            var usuarioFormateado = FormatearADTO(resultados.SingleOrDefault());
            return usuarioFormateado;

        }

        private async Task <InicioSesionDTO?> ValidarAccesoAsync(DatosUsuarioDTO datosUsuarioDTO, int idUsuario)
        {
            var resultados = await _context.inicioSesion
                .FromSqlInterpolated($@"
                    EXEC dbo.Sp_LoginNovedades 
                        @User = {datosUsuarioDTO.Usuario}, 
                        @Password = {datosUsuarioDTO.contrasena}, 
                        @IdSistema = {1}, 
                        @IdPerfil = {idUsuario}, 
                        @Accion = {2}")
                .ToListAsync();

            var usuarioFormateado = FormatearADTO(resultados.SingleOrDefault());

            return usuarioFormateado;
        }


        private InicioSesionDTO FormatearADTO (InicioSesion usuario)
        {

            return new InicioSesionDTO
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


        public string GenerarToken(DatosUsuarioDTO datosUsuarioDTO, int idUsuario)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, datosUsuarioDTO.Usuario ?? ""),
        new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "FrontEquinoterapia",
                audience: "FrontEquinoterapia",
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
