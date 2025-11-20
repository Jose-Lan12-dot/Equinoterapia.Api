//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.Data.Models.Evaluacion_;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Client;
//using Newtonsoft.Json.Bson;
//using System.Threading.Tasks;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class EvaluacionService
//    {

//        private readonly AppDbContext _context;
//        private readonly RespuestasService _respuestasService;

//        public EvaluacionService(AppDbContext context, RespuestasService respuestasService)
//        {
//            _context = context;
//            _respuestasService = respuestasService;
//        }

//        //////////////////////
//        public async Task<(GetEvaluacionDto?, string?, string)> GetAsync(int pacienteId)
//        {
//            var mensaje = "";
//            if (!await PacienteExist(pacienteId))
//            {
//                mensaje = "Paciente no encontrado";
//                return (null, "", mensaje);
//            }

//            var evaluacion = await GetEvaluacion(pacienteId);
//            if (IsNull(evaluacion))
//            {
//                mensaje = "Evaluacion no encontrada";
//                return (null, "", mensaje);
//            }

//            var nombrePaciente = await GetNombrePaciente(pacienteId);
//            var preguntas = Enumerable.Empty<PreguntasDto>();

//            (preguntas, mensaje) = await GetPreguntasAsync();
//            if (preguntas.Count() == 0)
//            {
//                return (null, "", mensaje);
//            }

//            var respuestas = Enumerable.Empty<RespuestasDto>();
//            (respuestas, mensaje) = await _respuestasService.Get(evaluacion!.EvaluacionId);
//            if (respuestas.Count() == 0)
//            {
//                return (null, "", mensaje);
//            }


//            return (new GetEvaluacionDto
//            {
//                Fecha = evaluacion.Fecha,
//                Preguntas = preguntas,
//                Respuestas = respuestas
//            }, nombrePaciente, "Evaluacion Encontrada");

//        }

//        //////////////////////
//        public async Task<(bool, string)> AddAsync(GetEvaluacionDto evaluacionDto, int pacienteId)
//        {

//            var mensaje = "";
//            if (!await PacienteExist(pacienteId))
//            {
//                mensaje = "Paciente no encontrado";
//                return (false, mensaje);
//            }


//            if (ValidateDataEvaluacion(evaluacionDto))
//            {
//                mensaje = "Datos de evaluación inválidos";
//                return (false, mensaje);
//            }

//            var numeroPreguntas = await _context.preguntas.CountAsync();
//            var numeroRespuestas = evaluacionDto.Respuestas.Count();
//            if (numeroPreguntas != numeroRespuestas)
//            {
//                mensaje = "El número de respuestas no coincide con el número de preguntas";
//                return (false, mensaje);
//            }


//            if (await EvaluacionExist(pacienteId))
//            {
//                mensaje = "Ya existe un evaluacion para este paciente";
//                return (false, mensaje);
//            }

//            var nuevaEvaluacion = new Evaluacion
//            {
//                PacienteId = pacienteId,
//                Fecha = evaluacionDto.Fecha
//            };
//            _context.evaluacion.AddInforme(nuevaEvaluacion);


//            try
//            {
//                await _context.SaveChangesAsync();
//                var exito = false;
//                (exito, mensaje) = await _respuestasService.AddInforme(evaluacionDto.Respuestas, nuevaEvaluacion.EvaluacionId);
//                if (!exito)
//                {
//                    return (false, mensaje);
//                }

//                return (true, "Evaluacion guardada correctamente");
//            }
//            catch (Exception ex)
//            {
//                mensaje = "Error al guardar la evaluacion: " + ex.Message;
//                return (false, mensaje);
//            }
//        }

//        //////////////////////
//        public async Task<(bool, string)> UpdateAsync(GetEvaluacionDto evaluacionDto, int pacienteId)
//        {
//            var mensaje = "";
//            if (!await PacienteExist(pacienteId))
//            {
//                mensaje = "Paciente_ no encontrado";
//                return (false, mensaje);
//            }


//            if (ValidateDataEvaluacion(evaluacionDto))
//            {
//                mensaje = "Datos de evaluación inválidos";
//                return (false, mensaje);
//            }

//            var numeroPreguntas = await _context.preguntas.CountAsync();
//            var numeroRespuestas = evaluacionDto.Respuestas.Count();
//            if (numeroPreguntas != numeroRespuestas)
//            {
//                mensaje = "El número de respuestas no coincide con el número de preguntas";
//                return (false, mensaje);
//            }

//            var evaluacion = await GetEvaluacion(pacienteId);
//            if (IsNull(evaluacion))
//            {
//                mensaje = "La evaluación no existe";
//                return (false, mensaje);
//            }

//            evaluacion!.Fecha = evaluacionDto.Fecha;
//            await _context.SaveChangesAsync();


//            try
//            {
//                var exito = false;
//                (exito, mensaje) = await _respuestasService.Update(evaluacionDto.Respuestas, evaluacion!.EvaluacionId);
//                if (!exito)
//                {
//                    return (false, mensaje);
//                }
//                return (true, "Evaluacion actualizada correctamente");
//            }
//            catch (Exception ex)
//            {
//                mensaje = "Error al actualizar la evaluacion: " + ex.Message;
//                return (false, mensaje);
//            }
//        }

//        //////////////////////
//        public async Task<(bool, string)> DeleteAsync(int pacienteId)
//        {

//            var mensaje = "";
//            if (!await PacienteExist(pacienteId))
//            {
//                mensaje = "Paciente no encontrado";
//                return (false, mensaje);
//            }

//            var evaluacion = await GetEvaluacion(pacienteId);
//            if (IsNull(evaluacion))
//            {
//                mensaje = "La evaluacion no exite";
//                return (false, mensaje);
//            }

//            try
//            {
//                var exito = false;
//                (exito, mensaje) = await _respuestasService.Delete(evaluacion!.EvaluacionId);
//                if (!exito)
//                {
//                    return (false, mensaje);
//                }
//                _context.evaluacion.Remove(evaluacion!);
//                await _context.SaveChangesAsync();

//                return (true, "Evaluacion eliminada correctamente");
//            }
//            catch (Exception ex)
//            {
//                mensaje = "Error al eliminar la evaluacion: " + ex.Message;
//                return (false, mensaje);
//            }

//        }

//        //////////////////////
//        public async Task<(IEnumerable<PreguntasDto>, string)> GetPreguntasAsync()
//        {

//            var preguntas = await _context.preguntas
//                .OrderBy(p => p.PreguntaId)
//                .Select(p => new PreguntasDto
//                {
//                    Orden = p.PreguntaId,
//                    Pregunta = p.Pregunta,
//                })
//                .ToListAsync();

//            if (preguntas.Count == 0)
//            {
//                var mensaje = "Preguntas no ncontradas";
//                return (preguntas, mensaje);
//            }

//            return (preguntas, "Preguntas encontradas");
//        }













//        //////////////////////
//        private async Task<Evaluacion?> GetEvaluacion(int pacienteId)
//        {
//            return await _context.evaluacion
//                .FirstOrDefaultAsync(e => e.PacienteId == pacienteId);
//        }

//        //////////////////////
//        private async Task<string?> GetNombrePaciente(int pacienteId)
//        {
//            return await _context.paciente
//                .Where(p => p.PacienteId == pacienteId)
//                .Select(p => p.Nombre)
//                .FirstOrDefaultAsync();
//        }

//        //////////////////////
//        private async Task<bool> PacienteExist(int pacienteId)
//        {
//            return await _context.paciente.AnyAsync(p => p.PacienteId == pacienteId);
//        }

//        //////////////////////
//        private async Task<bool> EvaluacionExist(int pacienteId)
//        {
//            return await _context.evaluacion.AnyAsync(e => e.PacienteId == pacienteId);
//        }




//        //////////////////////
//        private bool IsNull(object? objeto)
//        {
//            if (objeto == null)
//            {
//                return true;
//            }
//            return false;
//        }

//        //////////////////////
//        public bool ValidateDataEvaluacion(GetEvaluacionDto evaluacionDto)
//        {
//            if (IsNull(evaluacionDto)
//                || IsNull(evaluacionDto.Respuestas)
//)
//            {
//                return true;
//            }
//            return false;
//        }
//    }
//}
