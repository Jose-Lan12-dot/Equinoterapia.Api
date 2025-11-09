using Equinoterapia.Application.DTOs;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class CaballoService : ICaballoService
    {

        private readonly ICaballoRepository _caballoRepository;
        private readonly ICaballoQueryRepository _caballoQueryRepository;

        private readonly ISesionRepository _sesionRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;

        public CaballoService(ICaballoRepository caballoRepository, 
            ICaballoQueryRepository caballoQueryRepository,
            ISesionRepository sesionRepository,
            IAsistenciaRepository asistenciaRepository)
        {
            _caballoRepository = caballoRepository;
            _caballoQueryRepository = caballoQueryRepository;

            _sesionRepository = sesionRepository;
            _asistenciaRepository = asistenciaRepository;
        }

        public async Task<(IEnumerable<CaballoDto>, string)> GetAllAsync()
        {
            var caballos = new List<CaballoDto>();
            try
            {
                // Usamos el repositorio de consulta optimizado
                caballos = await _caballoQueryRepository.GetAllDtosAsync();
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<CaballoDto>(), $"Error al obtener los caballos: {ex.Message}");
            }

            if (!caballos.Any())
            {
                return (Enumerable.Empty<CaballoDto>(), "No se encontro ningun caballo");
            }

            return (caballos, "Lista de caballos devuelta satisfactoriamente");
        }

        public async Task<(CaballoDto?, string)> GetByIdAsync(int caballoId)
        {
            Caballo? caballo;
            try
            {
                caballo = await _caballoRepository.GetByIdAsync(caballoId);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener el caballo: {ex.Message}");
            }

            if (IsNull(caballo))
            {
                return (null, "Caballo no encontrado");
            }

            return (MapToCaballoDto(caballo), "Caballo encontrado");
        }

        public async Task<(bool, string)> AddAsync(AddCaballoDto addCaballoDto)
        {
            if (IsNull(addCaballoDto) || IsNull(addCaballoDto.FechaHerraje))
            {
                return (false, "Datos del Caballo no aceptados");
            }

            var nuevoCaballo = new Caballo
            {
                Placa = addCaballoDto.Placa,
                Nombre = addCaballoDto.Nombre,
                Estado = EstadoCaballo.Disponible,
                FechaHerraje = addCaballoDto.FechaHerraje!.Value,
                Padecimiento = addCaballoDto.Padecimiento,
            };

            try
            {
                _caballoRepository.Add(nuevoCaballo);
                await _caballoRepository.SaveChangesAsync();
                return (true, "Caballo agregado correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar el caballo: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdatePartialAsync(CaballoDto caballoDto, int caballoId)
        {
            if (IsNull(caballoDto))
            {
                return (false, "Datos del Caballo no aceptados");
            }
            if (caballoDto.CaballoId != caballoId)
            {
                return (false, "El ID del caballo no coincide");
            }

            var caballo = await _caballoRepository.GetByIdAsync(caballoId);
            if (IsNull(caballo))
            {
                return (false, "Caballo no encontrado");
            }

            if (!IsEmpty(caballoDto.Placa))
            {
                caballo.Placa = caballoDto.Placa!;
            }
            if (!IsEmpty(caballoDto.Nombre))
            {
                caballo.Nombre = caballoDto.Nombre!;
            }
            if (!IsEmpty(caballoDto.Estado))
            {
                caballo.Estado = GetEstado(caballoDto.Estado!, caballo.Estado);
            }
            if (!IsNull(caballoDto.FechaHerraje))
            {
                caballo.FechaHerraje = caballoDto.FechaHerraje!.Value;
            }
            if (!IsEmpty(caballoDto.Padecimiento))
            {
                caballo.Padecimiento = caballoDto.Padecimiento!;
            }

            try
            {
                // Usamos el repositorio
                await _caballoRepository.SaveChangesAsync();
                return (true, "Caballo actualizado correctamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el caballo: {ex.Message}");
            }
        }




        public async Task<(bool, string)> DeleteAsync(int caballoId)
        {
            // 1. Obtener la entidad principal (Caballo)
            var caballo = await _caballoRepository.GetByIdAsync(caballoId);
            if (IsNull(caballo))
            {
                return (false, "Caballo no encontrado");
            }

            try
            {
                // 2. Encontrar hijos (Sesiones)
                var sesiones = await _sesionRepository.GetByCaballoIdAsync(caballoId);
                if (sesiones.Any())
                {
                    // 3. Encontrar nietos (Asistencias)
                    var sesionIds = sesiones.Select(s => s.SesionId).ToList();
                    var asistencias = await _asistenciaRepository.GetBySesionIdsAsync(sesionIds);

                    if (asistencias.Any())
                    {
                        _asistenciaRepository.RemoveRange(asistencias);
                    }

                    // 4. Borrar hijos (Sesiones)
                    _sesionRepository.RemoveRange(sesiones);
                }

                // 5. Borrar la entidad principal (Caballo)
                _caballoRepository.Delete(caballo);

                // 6. ¡GUARDAR TODO EN UNA SOLA TRANSACCIÓN!
                await _caballoRepository.SaveChangesAsync();

                return (true, "Caballo y sus sesiones/asistencias asociadas eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el caballo: {ex.Message}");
            }
        }



        // --- MÉTODOS PRIVADOS (Helper) ---

        private bool IsNull(object? objeto) => objeto is null;

        private CaballoDto MapToCaballoDto(Caballo caballo)
        {
            return new CaballoDto
            {
                CaballoId = caballo.CaballoId,
                Placa = caballo.Placa,
                Nombre = caballo.Nombre,
                Estado = caballo.Estado.ToString(),
                FechaHerraje = caballo.FechaHerraje,
                Padecimiento = caballo.Padecimiento,
            };
        }

        public bool IsEmpty(string? str) => string.IsNullOrWhiteSpace(str);

        private EstadoCaballo GetEstado(string estado, EstadoCaballo estadoActual)
        {
            if (Enum.TryParse<EstadoCaballo>(estado, out var estadoNuevo))
                return estadoNuevo;
            return estadoActual;
        }
    }
}