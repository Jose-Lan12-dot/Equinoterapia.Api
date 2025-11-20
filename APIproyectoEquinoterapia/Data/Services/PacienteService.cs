//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.Data.Models.Archivo_;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class PacienteService
//    {
//        private readonly AppDbContext _context;
//        private readonly ArchivoService _archivoService;

//        public PacienteService(AppDbContext context, ArchivoService archivoService)
//        {
//            _context = context;
//            _archivoService = archivoService;
//        }

////////////////////////
//        public async Task<(IEnumerable<GetAllPacienteDto>,string)> GetAllAsync()
//        {
//            var pacientes = new List<GetAllPacienteDto>();
//            try
//            {
//                pacientes = await GetAllPacientes();
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al obtener los pacientes: {ex.Message}";
//                return (Enumerable.Empty<GetAllPacienteDto>(), mensaje);
//            }


//            if (pacientes.Count == 0)
//            {
//                var mensaje = "No se encontro ningun paciente";
//                return (Enumerable.Empty<GetAllPacienteDto>(), mensaje);
//            }

//            return (pacientes, "Pacientes devueltos con exito");
//        }

////////////////////////
//        public async Task<(UpdatePacienteDto?,int,bool,List<string>?,string)> GetByIdAsync(int pacienteId)
//        {
//            var paciente = null as Paciente;
//            try
//            {
//                paciente = await GetPacienteById(pacienteId);
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al obtener el paciente: {ex.Message}";
//                return (null,0,false,null,mensaje);
//            }
//            if (IsNull(paciente))
//            {
//                var mensaje = "Paiente no encontrado";
//                return (null, 0, false, null, mensaje);
//            }

//            var numeroFaltas = await GetFaltas(pacienteId);
//            var faltaEvaluacion = await HasEvaluacion(pacienteId);
//            List<string> archivosPendientes = await GetFileMissing(pacienteId);

//            return (MapToDto(paciente!),numeroFaltas,faltaEvaluacion,archivosPendientes,"Paciente encontrado");
//        }

////////////////////////
//        public async Task<(bool, string)> AddAsync(AddPacienteConArchivosDto addPaciente)
//        {
//            var paciente = addPaciente.Paciente;
//            var archivos = addPaciente.Archivos;
//            var nuevoPaciente = new Paciente
//            {
//                Nombre = paciente.Nombre,
//                FechaNacimiento = paciente.FechaNacimiento,
//                Tutor = paciente.Tutor,
//                TelefonoTutor = paciente.TelefonoTutor,
//                Genero = GetGenero(paciente.Genero,Generos.NoBinario),
//                Estado = EstadoPaciente.Activo,
//                FechaRegistro = DateTime.UtcNow
//            };

//            try { 
//                _context.paciente.AddInforme(nuevoPaciente);
//                await _context.SaveChangesAsync();
                
//                if (!IsNull(archivos) && archivos.Count > 0)
//                {
//                    if (await AddArchivos(archivos,nuevoPaciente.PacienteId))
//                    {
//                        var mensajeCompleto = "Paciente creado, " +
//                            "pero hubo errores al agregar algunos archivos";
//                        return (true, mensajeCompleto);
//                    }
//                }

//                return (true, "Datos y archivos paciente agregado Correctamente");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al crear el paciente: {ex.Message}";
//                return (false, mensaje);
//            }

//        }

////////////////////////
//        public async Task<(bool, string)> UpdateAllAsync(UpdatePacienteDto pacienteDto, int pacienteId)
//        {
//            if (pacienteDto.PacienteId != pacienteId)
//            {
//                var mensaje = "El ID del paciente no coincide con el ID de la ruta";
//                return (false, mensaje);
//            }

//            if (ValidatePacienteData(pacienteDto))
//            {
//                var mensaje = "Datos incompletos para actualizar el paciente";
//                return (false, mensaje);
//            }

//            var paciente = await GetPacienteById(pacienteId);
//            if (IsNull(paciente))
//            {
//                var mensaje = "Paciente no encontrado";
//                return (false, mensaje);
//            }

//            paciente!.Nombre = pacienteDto.Nombre!;
//            paciente.FechaNacimiento = pacienteDto.FechaNacimiento!.Value;
//            paciente.Tutor = pacienteDto.Tutor!;
//            paciente.TelefonoTutor = pacienteDto.TelefonoTutor!;
//            paciente.Estado = GetEstado(pacienteDto.Estado!, paciente.Estado);
//            paciente.Genero = GetGenero(pacienteDto.Genero!, paciente.Genero);

//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Paciente actualizado de forma exitosa");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al actualizar el paciente: {ex.Message}";
//                return (false, mensaje);
//            }
//        }

////////////////////////
//        public async Task<(bool, string)> UpdateAsync(UpdatePacienteDto pacienteDto, int pacienteId)
//        {
//            if (pacienteDto.PacienteId != pacienteId)
//            {
//                var mensaje = "El ID del paciente no coincide con el ID de la ruta";
//                return (false, mensaje);
//            }

//            var paciente = await GetPacienteById(pacienteId);
//            if (IsNull(paciente))
//            {
//                var mensaje = "Paciente no encontrado";
//                return (false, mensaje);
//            }

//            if(!IsEmpty(pacienteDto.Nombre))
//            {
//                paciente!.Nombre = pacienteDto.Nombre!;
//            }

//            if(!IsNull(pacienteDto.FechaNacimiento))
//            {
//                paciente!.FechaNacimiento = pacienteDto.FechaNacimiento!.Value;
//            }

//            if(!IsEmpty(pacienteDto.Tutor))
//            {
//                paciente!.Tutor = pacienteDto.Tutor!;
//            }

//            if (!IsEmpty(pacienteDto.TelefonoTutor))
//            {
//                paciente!.TelefonoTutor = pacienteDto.TelefonoTutor!;
//            }

//            if (!IsEmpty(pacienteDto.Estado))
//            {
//                paciente!.Estado = GetEstado(pacienteDto.Estado!, paciente.Estado);
//            }

//            if (!IsEmpty(pacienteDto.Genero))
//            {
//                paciente.Genero = GetGenero(pacienteDto.Genero!, paciente.Genero);
//            }


//            try
//            {
//                await _context.SaveChangesAsync();
//                return (true, "Paciente actualizado de forma exitosa");
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al actualizar el paciente: {ex.Message}";
//                return (false, mensaje);
//            }
//        }

//        //////////////////////
//        //public async Task<(bool, string)> DeleteAsync(int pacienteId)
//        //{
//        //    var paciente = await GetPacienteById(pacienteId);
//        //    if (IsNull(paciente))
//        //    {
//        //        var mensaje = "Paciente no encontrado";
//        //        return (false, mensaje);
//        //    }

//        //    var tieneSesiones = await InstructorHasSesiones(instructorId);
//        //    if (tieneSesiones)
//        //    {
//        //        var mensaje = "No se puede eliminar el paciente porque tiene sesiones asociadas.";
//        //        return (false, mensaje);
//        //    }

//        //    try
//        //    {
//        //        _context.instructor.Remove(paciente!);
//        //        await _context.SaveChangesAsync();
//        //        return (true, "Instructor eliminado correctamente");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        var mensaje = $"Error al eliminar el paciente: {ex.Message}";
//        //        return (false, mensaje);
//        //    }
//        //}








////////////////////////
      
        
        
        
        
        
        
        
        
        
        
//        private async Task<List<string>> GetFileMissing(int pacienteId)
//        {
//            var archivosPaciente = await _context.archivo
//                .Where(a => a.PacienteId == pacienteId)
//                .Select(a => a.TiposArchivo)
//                .ToListAsync();
//            var archivosRequeridos = await _context.tipoArchivo.ToListAsync();

//            var archivosFaltantes = archivosRequeridos
//                .Where(ar => !archivosPaciente.Contains(ar))
//                .Select(ar => ar.Nombre)
//                .ToList();

//            return archivosFaltantes;
//        }

////////////////////////
//        private async Task<List<GetAllPacienteDto>> GetAllPacientes()
//        {
//            var pacientes = await _context.paciente.ToListAsync();
//            var listaDto = new List<GetAllPacienteDto>();
//            var numeroArchivosRequeridos = await _context.tipoArchivo.CountAsync();

//            foreach (var p in pacientes)
//            {
//                var dto = new GetAllPacienteDto
//                {
//                    PacienteId = p.PacienteId,
//                    Nombre = p.Nombre,
//                    Tutor = p.Tutor,
//                    Estado = p.Estado.ToString(),
//                    NumeroFaltas = await GetFaltas(p.PacienteId),
//                    FormularioHecho = await HasEvaluacion(p.PacienteId),
//                    FaltanArchivos = await MissingFiles(p.PacienteId, numeroArchivosRequeridos)
//                };
//                listaDto.AddInforme(dto);
//            }

//            return listaDto;
//        }

////////////////////////
//        private async Task<int> GetFaltas(int pacienteId)
//        {
//            return await _context.asistencia
//                .CountAsync(c => c.PacienteId == pacienteId && c.Asistio == false);
//        }

////////////////////////
//        private async Task<bool> HasEvaluacion(int pacienteId)
//        {
//            return await _context.evaluacion
//                .AnyAsync(f => f.PacienteId == pacienteId);
//        }

////////////////////////
//        private async Task<bool> MissingFiles(int pacienteId, int numeroArchivosRequeridos)
//        {
//            var numeroArchivosPaciente = await _context.archivo
//                .CountAsync(a => a.PacienteId == pacienteId);
//            if (numeroArchivosPaciente < numeroArchivosRequeridos)
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private async Task<Paciente?> GetPacienteById(int pacienteId)
//        {
//            return await _context.paciente.FindAsync(pacienteId);
//        }





////////////////////////
//        private bool IsNull(object? obj)
//        {
//            if(obj == null)
//                return true;
//            return false;
//        }//

////////////////////////
//        private bool IsEmpty(string? str)
//        {
//            return string.IsNullOrWhiteSpace(str);
//        }//

////////////////////////
//        private UpdatePacienteDto MapToDto(Paciente paciente)
//        {
//            return new UpdatePacienteDto
//            {
//                PacienteId = paciente.PacienteId,
//                Nombre = paciente.Nombre,
//                FechaNacimiento = paciente.FechaNacimiento,
//                Tutor = paciente.Tutor,
//                TelefonoTutor = paciente.TelefonoTutor,
//                Estado = paciente.Estado.ToString(),
//                Genero = paciente.Genero.ToString()
//            };
//        }//

////////////////////////
//        private bool ValidatePacienteData(AddPacienteDto AddpacienteDto)
//        {
//            if(IsEmpty(AddpacienteDto.Nombre)
//                || IsEmpty(AddpacienteDto.Tutor)
//                || IsEmpty(AddpacienteDto.TelefonoTutor)
//                || IsEmpty(AddpacienteDto.Genero)
//                || IsNull(AddpacienteDto.FechaNacimiento))
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private bool ValidatePacienteData(UpdatePacienteDto pacienteDto)
//        {
//            if(IsEmpty(pacienteDto.Nombre)
//                || IsEmpty(pacienteDto.Tutor)
//                || IsEmpty(pacienteDto.TelefonoTutor)
//                || IsEmpty(pacienteDto.Estado)
//                || IsEmpty(pacienteDto.Genero)
//                || IsNull(pacienteDto.FechaNacimiento))
//            {
//                return true;
//            }
//            return false;
//        }//

////////////////////////
//        private Generos GetGenero(string genero, Generos generoActual)
//        {
//            if (Enum.TryParse<Generos>(genero, out var generoValido))
//            {
//                return generoValido;
//            }
//            return generoActual;
//        }//

////////////////////////
//        private EstadoPaciente GetEstado(string estado, EstadoPaciente estadoActual)
//        {
//            if (Enum.TryParse<EstadoPaciente>(estado, out var estadoNuevo))
//            {
//                return estadoNuevo;
//            }
//            return estadoActual;
//        }//

////////////////////////
//        private async Task<bool> AddArchivos(List<UpdateArchivoDto> archivos, int pacienteId)
//        {
//            var mensajesArchivos = new List<string>();
//            foreach (var archivo in archivos)
//            {
//                var (exito, mensaje) = await _archivoService.AddAsync(archivo, pacienteId);
//                if (!exito)
//                    mensajesArchivos.AddInforme(mensaje);
//            }
//            if (mensajesArchivos.Count > 0)
//                return true;
//            return false;
//        }//

//    }
//}