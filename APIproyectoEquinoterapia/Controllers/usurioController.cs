using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usurioController : ControllerBase
    {

        private readonly AppDbContext _context;

        public usurioController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    if (request.Accion == 1)
        //    {
        //        var existe = await _context.Cat_Usuario
        //            .AnyAsync(u => u.UsuarioNombre == request.User && u.Activo);

        //        if (!existe)
        //            return NotFound(new LoginResponse { Mensaje = "Usuario no existe" });

        //        var perfil = await (from ps in _context.Tbl_PerfilSistema
        //                            join pu in _context.Cat_PerfilUsuario on request.IdSistema equals pu.IdSistema
        //                            join cu in _context.Cat_Usuario on request.User equals cu.UsuarioNombre
        //                            where ps.IdUsuario == cu.IdUsuario && cu.Activo
        //                            select new LoginResponse
        //                            {
        //                                IdUsuario = cu.IdUsuario,
        //                                IdKronos = 0,
        //                                Usuario = cu.UsuarioNombre,
        //                                NombreCompleto = "",
        //                                Mensaje = "",
        //                                Foto = null,
        //                                IdPerfil = ps.IdPerfil,
        //                                Perfil = pu.Descripcion
        //                            }).FirstOrDefaultAsync();

        //        return Ok(perfil ?? new LoginResponse { Mensaje = "Perfil no encontrado" });
        //    }

        //    if (request.Accion == 2)
        //    {
        //        var usuario = await (from cu in _context.Cat_Usuario
        //                             join ps in _context.Tbl_PerfilSistema on cu.IdUsuario equals ps.IdUsuario
        //                             join pu in _context.Cat_PerfilUsuario on ps.IdPerfil equals pu.IdPerfilUsuario
        //                             join fe in _context.vw_FotoEmpleado on cu.IdKronos equals fe.Id
        //                             where cu.UsuarioNombre == request.User && cu.Password == request.Password && cu.Activo
        //                             select new LoginResponse
        //                             {
        //                                 IdUsuario = cu.IdUsuario,
        //                                 IdKronos = cu.IdKronos,
        //                                 Usuario = cu.UsuarioNombre,
        //                                 NombreCompleto = fe.NombreCompleto,
        //                                 Mensaje = "Login Correcto",
        //                                 Foto = fe.Foto,
        //                                 IdPerfil = request.IdPerfil,
        //                                 Perfil = ""
        //                             }).FirstOrDefaultAsync();

        //        if (usuario == null)
        //            return Unauthorized(new LoginResponse { Mensaje = "Usuario o contraseña incorrectos" });

        //        return Ok(usuario);
        //    }

        //    return BadRequest(new LoginResponse { Mensaje = "Acción no válida" });
        //}
    }
}
