using APIproyectoEquinoterapia.Data.Services.Paciente_;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIproyectoEquinoterapia.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }


//////////////////////
        [HttpGet("obtener_todos")]
        public async Task<ActionResult<IEnumerable<AllPacienteDto>>> GetAll()
        {
            var (pacientes,mensaje) = await _pacienteService.GetAllAsync();
            if (pacientes == null || !pacientes.Any())
                return NotFound(mensaje);
            return Ok(pacientes);
        }

//////////////////////
        [HttpGet("obtener-por-id/{pacienteId}")]
        public async Task<ActionResult<PacienteDto>> GetById(int pacienteId)
        {
            var (paciente, numeroFaltas, 
                faltaEvaluacion, archivosPendientes,mensaje) = await _pacienteService.GetByIdAsync(pacienteId);
            if (paciente == null)
                return NotFound(mensaje);
            return Ok(new {
                paciente,
                numeroFaltas,
                faltaEvaluacion,
                archivosPendientes,
                mensaje
            });
        }

//////////////////////
        [HttpPost("agregar")]
        public async Task<ActionResult<ArchivoDto>> Add(AddPacienteConArchivosDto pacienteData)
        {
            var (exito, mensaje) = await _pacienteService.AddAsync(pacienteData);
            if (!exito)
                return Conflict(mensaje);

            return Ok(mensaje);
        }

        //////////////////////
        [HttpPut("actualizar-todo/{pacienteId}")]
        public async Task<IActionResult> UpdateAll(PacienteDto pacienteDto, int pacienteId)
        {

            var (exito,mensaje) = await _pacienteService.UpdateAllAsync(pacienteDto,pacienteId);
            if (!exito)
                return NotFound(mensaje);

            return Ok(mensaje);
        }

//////////////////////
        [HttpPatch("actualizar-parcialmente/{pacienteId}")]
        public async Task<IActionResult> UpdatePartial(PacienteDto pacienteDto, int pacienteId)
        {
            var (exito,mensaje) = await _pacienteService.UpdatePartialAsync(pacienteDto,pacienteId);
            if (!exito)
                return NotFound(mensaje);
            return Ok(mensaje);
        }





        //[HttpDelete("eliminar/{id}")]
        //public async Task<IActionResult> DeletePaciente(int pacienteId)
        //{
        //    var (exito, mensaje) = await _pacienteService.DeletePacienteAsync(pacienteId);
        //    if (!exito)
        //        return NotFound(mensaje);

        //    return Ok(mensaje);
        //}


    }
}
