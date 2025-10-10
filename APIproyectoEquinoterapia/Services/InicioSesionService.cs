using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace APIproyectoEquinoterapia.Services
{
    public class InicioSesionService
    {
        private readonly  AppDbContext _context;

        public InicioSesionService(AppDbContext context)
        {
            _context = context;
        }


        public async Task <(bool,string)> PostInicioSesionAsync(DatosUsuarioDTO datosUsuarioDTO)
        {
            var usuario = await ValidarUsuarioAsync(datosUsuarioDTO);
            if (usuario == null || usuario.IdUsuario == 0)
                return (false,"Usuario no encontrado");

            var acceso = await ValidarAccesoAsync(datosUsuarioDTO, usuario.IdUsuario);
            if (acceso == null || acceso.IdUsuario == 0)
                return (false,"Credenciales incorrectas");

            return (true,"Usuario encontrado");
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
    }
}
