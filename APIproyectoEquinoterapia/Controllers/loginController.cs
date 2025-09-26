using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public loginController (AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginEntrada request)
        {
            var resultado = await _context.loginrespuesta
                .FromSqlRaw("EXEC Sp_LoginNovedades @User={0}, @Password={1}, @IdSistema={2}, @IdPerfil={3}, @Accion={4}",
                            request.User, request.Password, request.IdSistema, request.IdPerfil, request.Accion)
                .ToListAsync();

            var respuesta = resultado.FirstOrDefault();

            if (respuesta == null || respuesta.IdUsuario == 0)
                return Unauthorized(respuesta);

            return Ok(respuesta);
        }
    }
}
