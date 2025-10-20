using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace APIproyectoEquinoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly InicioSesionService _inicioSesionService;

        public InicioSesionController (InicioSesionService inicioSesionService)
        {
            _inicioSesionService = inicioSesionService;  
        }

        [HttpPost("acceso")]
        public async Task<IActionResult> PostInicioSesion(InicioSesionDataDto inicioSesionDataDto)
        {
            var (exito,mensaje,token) =  await _inicioSesionService.LoginAsync(inicioSesionDataDto);

            if (!exito)
                return BadRequest(mensaje);

            return Ok(new{token,mensaje});
        }



    }
}
