using Equinoterapia.Application.DTOs.Auth;
using Equinoterapia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinoterapia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IInicioSesionService _inicioSesionService;

        public AuthController(IInicioSesionService inicioSesionService)
        {
            _inicioSesionService = inicioSesionService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredencialesDto inicioSesionDataDto)
        {

            var (exito, mensaje, data) = await _inicioSesionService.LoginAsync(inicioSesionDataDto);

            if (!exito)
            {
                return Unauthorized( mensaje );
            }

            return Ok(data);
        }
        [HttpGet("obtener-perfil")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPerfil([FromQuery] CredencialesDto inicioSesionDataDto)
        {

            var (exito, nombreUsuario, foto) = await _inicioSesionService.ObtenerPerfilAsync(inicioSesionDataDto);

            if (!exito)
            {
                return Unauthorized( "No se pudo obtener el perfil" );
            }

            return Ok(new {nombreUsuario = nombreUsuario, foto = foto });
        }
    }
}