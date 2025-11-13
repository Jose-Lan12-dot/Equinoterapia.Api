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

            var (exito, mensaje, token) = await _inicioSesionService.LoginAsync(inicioSesionDataDto);

            if (!exito)
            {
                return Unauthorized(new { Mensaje = mensaje });
            }

            return Ok(new { Token = token, Mensaje = mensaje });
        }
    }
}