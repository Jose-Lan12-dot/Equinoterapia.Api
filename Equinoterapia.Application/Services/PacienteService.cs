using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPacienteQueryRepository _pacienteQueryRepository;
        private readonly IArchivoService _archivoService;

        // --- ¡NUEVAS DEPENDENCIAS PARA DELETE! ---
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IArchivoRepository _archivoRepository; 
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IRespuestaRepository _respuestaRepository;

        public PacienteService(
            IPacienteRepository pacienteRepository,
            IPacienteQueryRepository pacienteQueryRepository,
            IArchivoService archivoService,

            IAsistenciaRepository asistenciaRepository,
            IArchivoRepository archivoRepository,
            IEvaluacionRepository evaluacionRepository,
            IRespuestaRepository respuestaRepository)
        {
            _pacienteRepository = pacienteRepository;
            _pacienteQueryRepository = pacienteQueryRepository;
            _archivoService = archivoService;

            _asistenciaRepository = asistenciaRepository;
            _archivoRepository = archivoRepository;
            _evaluacionRepository = evaluacionRepository;
            _respuestaRepository = respuestaRepository;
        }

        public async Task<(IEnumerable<AllPacienteDto>, string)> GetAllAsync()
        {


            var pacientes = await _pacienteQueryRepository.GetAllAsync();

            if (!pacientes.Any())
            {
                return (Enumerable.Empty<AllPacienteDto>(), "No se encontro ningun paciente");
            }


            return (pacientes, "Pacientes devueltos con exito");
        }

        public async Task<(PacienteDto?, int, bool, List<string>?, string)> GetByIdAsync(int pacienteId)
        {

            var paciente = await _pacienteRepository.GetByIdAsync(pacienteId);

            if (paciente == null)
            {
                return (null, 0, false, null, "Paciente no encontrado");
            }


            var numeroFaltas = await _pacienteRepository.GetFaltasCountAsync(pacienteId);
            var faltaEvaluacion = !await _pacienteRepository.HasEvaluacionAsync(pacienteId); // Tu código original decía faltaEvaluacion, así que asumo que 'true' es si falta
            List<string> archivosPendientes = await _pacienteRepository.GetMissingFileTypesAsync(pacienteId);

            return (MapToDto(paciente), numeroFaltas, faltaEvaluacion, archivosPendientes, "Paciente encontrado");
        }

        public async Task<(bool, string)> AddAsync(AddPacienteConArchivosDto addPaciente)
        {
            var pacienteDto = addPaciente.Paciente;
            var archivos = addPaciente.Archivos;

            // La validación de DTOs (como ValidatePacienteData)
            // la moveremos a 'FluentValidation' después. Por ahora la dejamos.
            if (ValidatePacienteData(pacienteDto))
            {
                return (false, "Datos incompletos");
            }


            var nuevoPaciente = new Paciente
            {
                Nombre = pacienteDto.Nombre,
                FechaNacimiento = pacienteDto.FechaNacimiento,
                Tutor = pacienteDto.Tutor,
                TelefonoTutor = pacienteDto.TelefonoTutor,
                Genero = GetGenero(pacienteDto.Genero, Generos.NoBinario),
                Estado = EstadoPaciente.Activo,
                FechaRegistro = DateTime.UtcNow
            };

            try
            {
                _pacienteRepository.Add(nuevoPaciente);
                await _pacienteRepository.SaveChangesAsync(); 

                if (archivos != null && archivos.Count > 0)
                {
                    if (await AddArchivos(archivos, nuevoPaciente.PacienteId))
                    {
                        return (true, "Paciente creado, pero hubo errores al agregar algunos archivos");
                    }
                }
                return (true, "Datos y archivos paciente agregado Correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear el paciente: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdateAllAsync(PacienteDto pacienteDto, int pacienteId)
        {
            if (pacienteDto.PacienteId != pacienteId)
            {
                return (false, "El ID del paciente no coincide con el ID de la ruta");
            }

            if (ValidatePacienteData(pacienteDto))
            {
                return (false, "Datos incompletos para actualizar el paciente");
            }

            // Obtenemos la entidad usando el repositorio
            var paciente = await _pacienteRepository.GetByIdAsync(pacienteId);
            if (paciente == null)
            {
                return (false, "Paciente no encontrado");
            }

            // Lógica de mapeo
            paciente.Nombre = pacienteDto.Nombre!;
            paciente.FechaNacimiento = pacienteDto.FechaNacimiento!.Value;
            paciente.Tutor = pacienteDto.Tutor!;
            paciente.TelefonoTutor = pacienteDto.TelefonoTutor!;
            paciente.Estado = GetEstado(pacienteDto.Estado!, paciente.Estado);
            paciente.Genero = GetGenero(pacienteDto.Genero!, paciente.Genero);

            try
            {
                // Solo guardamos. EF Core rastrea los cambios
                await _pacienteRepository.SaveChangesAsync();
                return (true, "Paciente actualizado de forma exitosa");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el paciente: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdatePartialAsync(PacienteDto pacienteDto, int pacienteId)
        {
            if (pacienteDto.PacienteId != pacienteId)
            {
                var mensaje = "El ID del paciente no coincide con el ID de la ruta";
                return (false, mensaje);
            }

            var paciente = await _pacienteRepository.GetByIdAsync(pacienteId);
            if (IsNull(paciente))
            {
                var mensaje = "Paciente no encontrado";
                return (false, mensaje);
            }

            if (!IsEmpty(pacienteDto.Nombre))
            {
                paciente!.Nombre = pacienteDto.Nombre!;
            }

            if (!IsNull(pacienteDto.FechaNacimiento))
            {
                paciente!.FechaNacimiento = pacienteDto.FechaNacimiento!.Value;
            }

            if (!IsEmpty(pacienteDto.Tutor))
            {
                paciente!.Tutor = pacienteDto.Tutor!;
            }

            if (!IsEmpty(pacienteDto.TelefonoTutor))
            {
                paciente!.TelefonoTutor = pacienteDto.TelefonoTutor!;
            }

            if (!IsEmpty(pacienteDto.Estado))
            {
                paciente!.Estado = GetEstado(pacienteDto.Estado!, paciente.Estado);
            }

            if (!IsEmpty(pacienteDto.Genero))
            {
                paciente.Genero = GetGenero(pacienteDto.Genero!, paciente.Genero);
            }


            try
            {
                await _pacienteRepository.SaveChangesAsync();
                return (true, "Paciente actualizado de forma exitosa");
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al actualizar el paciente: {ex.Message}";
                return (false, mensaje);
            }
        }


        public async Task<(bool, string)> DeleteAsync(int pacienteId)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(pacienteId);
            if (paciente == null)
            {
                return (false, "Paciente no encontrado");
            }

            try
            {
                //Encontrar y borrar hijos (Asistencias)
                var asistencias = await _asistenciaRepository.GetByPacienteIdAsync(pacienteId);
                if (asistencias.Any())
                {
                    _asistenciaRepository.RemoveRange(asistencias);
                }

                //Encontrar y borrar hijos (Archivos)
                var archivos = await _archivoRepository.GetByPacienteIdAsync(pacienteId);
                if (archivos.Any())
                {
                    _archivoRepository.RemoveRange(archivos);
                }

                //Encontrar y borrar hijos (Evaluacion y sus hijos Respuestas)
                var evaluacion = await _evaluacionRepository.GetEvaluacionByPacienteIdAsync(pacienteId);
                if (evaluacion != null)
                {
                    //Borrar nietos (Respuestas)
                    var respuestas = await _respuestaRepository.GetRespuestasByEvaluacionIdAsync(evaluacion.EvaluacionId);
                    if (respuestas.Any())
                    {
                        _respuestaRepository.RemoveRange(respuestas);
                    }
                    //Borrar hijo (Evaluacion)
                    _evaluacionRepository.Delete(evaluacion);
                }

                //Borrar la entidad principal (Paciente)
                _pacienteRepository.Delete(paciente);


                await _pacienteRepository.SaveChangesAsync();

                return (true, "Paciente y todos sus registros asociados eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el paciente: {ex.Message}");
            }
        }









        // --- MÉTODOS PRIVADOS (Helper) ---



        private async Task<bool> AddArchivos(List<ArchivoDto> archivos, int pacienteId)
        {
            var mensajesArchivos = new List<string>();
            foreach (var archivo in archivos)
            {
                // TODO: Refactorizar _archivoService para que use IArchivoService
                var (exito, mensaje) = await _archivoService.AddAsync(archivo, pacienteId);
                if (!exito)
                    mensajesArchivos.Add(mensaje);
            }
            return mensajesArchivos.Count > 0;
        }

        private bool IsNull(object? obj) => obj == null;
        private bool IsEmpty(string? str) => string.IsNullOrWhiteSpace(str);

        private PacienteDto MapToDto(Paciente paciente)
        {
            return new PacienteDto
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                FechaNacimiento = paciente.FechaNacimiento,
                Tutor = paciente.Tutor,
                TelefonoTutor = paciente.TelefonoTutor,
                Estado = paciente.Estado.ToString(),
                Genero = paciente.Genero.ToString()
            };
        }



        //////////////////////
        private bool ValidatePacienteData(AddPacienteDto AddpacienteDto)
        {
            if (IsEmpty(AddpacienteDto.Nombre)
                || IsEmpty(AddpacienteDto.Tutor)
                || IsEmpty(AddpacienteDto.TelefonoTutor)
                || IsEmpty(AddpacienteDto.Genero)
                || IsNull(AddpacienteDto.FechaNacimiento))
            {
                return true;
            }
            return false;
        }

        //////////////////////
        private bool ValidatePacienteData(PacienteDto pacienteDto)
        {
            if (IsEmpty(pacienteDto.Nombre)
                || IsEmpty(pacienteDto.Tutor)
                || IsEmpty(pacienteDto.TelefonoTutor)
                || IsEmpty(pacienteDto.Estado)
                || IsEmpty(pacienteDto.Genero)
                || IsNull(pacienteDto.FechaNacimiento))
            {
                return true;
            }
            return false;
        }//

        //////////////////////
        private Generos GetGenero(string genero, Generos generoActual)
        {
            if (Enum.TryParse<Generos>(genero, out var generoValido))
            {
                return generoValido;
            }
            return generoActual;
        }//

        //////////////////////
        private EstadoPaciente GetEstado(string estado, EstadoPaciente estadoActual)
        {
            if (Enum.TryParse<EstadoPaciente>(estado, out var estadoNuevo))
            {
                return estadoNuevo;
            }
            return estadoActual;
        }//
    }
}

