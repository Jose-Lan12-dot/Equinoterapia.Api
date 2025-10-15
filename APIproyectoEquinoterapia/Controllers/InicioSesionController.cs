using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> PostInicioSesion(DatosUsuarioDTO datosUsuarioDTO)
        {
            var (respuesta,mensaje) =  await _inicioSesionService.PostInicioSesionAsync(datosUsuarioDTO);

            if (!respuesta)
                return BadRequest(mensaje);

            return Ok(mensaje);
        }



    }
}
