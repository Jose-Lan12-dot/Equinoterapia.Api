using Equinoterapia.Application.DTOs.Caballos;
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

        public async Task<(IEnumerable<GetAllCaballoDto>, string)> GetAllAsync()
        {
            var caballos = new List<GetAllCaballoDto>();
            try
            {
                
                caballos = await _caballoQueryRepository.GetAllDtosAsync();
            }
            catch (Exception ex)
            {
                return (Enumerable.Empty<GetAllCaballoDto>(), $"Error al obtener los caballos: {ex.Message}");
            }

            if (!caballos.Any())
            {
                return (Enumerable.Empty<GetAllCaballoDto>(), "No se encontro ningun caballo");
            }

            return (caballos, "Lista de caballos devuelta satisfactoriamente");
        }

        public async Task<(GetCaballoDto?, string)> GetByIdAsync(int caballoId)
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

            return (MapToCaballoDto(caballo!), "Caballo encontrado");
        }

        public async Task<(bool, string)> AddAsync(AddCaballoDto addCaballoDto)
        {

            var nuevoCaballo = new Caballo
            {
                Placa = addCaballoDto.Placa,
                Nombre = addCaballoDto.Nombre,
                Estado = EstadoCaballo.Disponible,
                FechaHerraje = addCaballoDto.FechaHerraje!,
                HistorialMedico = addCaballoDto.Padecimientos.Select(p => new HistorialMedico
                {
                    PadecimientoId = p.PadecimientoId,
                    Observaciones = p.Observaciones,
                }).ToList()
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

        public async Task<(bool, string)> UpdatelAsync(UpdateCaballoDto caballoDto, int caballoId)
        {

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
            if (caballoDto.Padecimientos != null)
            {
                SincronizarHistorialMedico(caballo, caballoDto.Padecimientos);
            }

            try
            {
                
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
            
            var caballo = await _caballoRepository.GetByIdAsync(caballoId);
            if (IsNull(caballo))
            {
                return (false, "Caballo no encontrado");
            }

            try
            {
                
                var sesiones = await _sesionRepository.GetByCaballoIdAsync(caballoId);
                if (sesiones.Any())
                {
                    
                    var sesionIds = sesiones.Select(s => s.SesionId).ToList();
                    var asistencias = await _asistenciaRepository.GetBySesionIdsAsync(sesionIds);

                    if (asistencias.Any())
                    {
                        _asistenciaRepository.RemoveRange(asistencias);
                    }

               
                    _sesionRepository.RemoveRange(sesiones);
                }

                
                _caballoRepository.Delete(caballo);

                
                await _caballoRepository.SaveChangesAsync();

                return (true, "Caballo y sus sesiones/asistencias asociadas eliminados correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el caballo: {ex.Message}");
            }
        }

        public async Task<List<PadecimientosDto>> GetPadecimientosAsync()
        {
            return await _caballoQueryRepository.GetPadecimientosAsync();
        }



        // --- MÉTODOS PRIVADOS (Helper) ---

        private bool IsNull(object? objeto) => objeto is null;

        private GetCaballoDto MapToCaballoDto(Caballo caballo)
        {
            return new GetCaballoDto
            {
                CaballoId = caballo.CaballoId,
                Placa = caballo.Placa,
                Nombre = caballo.Nombre,
                Estado = caballo.Estado.ToString(),
                FechaHerraje = caballo.FechaHerraje,
                Padecimientos = caballo.HistorialMedico.Select(h => new HistorialMedicoDto
                {
                    HistorialId = h.Id,
                    PadecimientoId = h.PadecimientoId,
                    NombrePadecimiento = h.Padecimiento.PadecimientoNombre,
                    Observaciones = h.Observaciones
                }).ToList()
            };
        }

        public bool IsEmpty(string? str) => string.IsNullOrWhiteSpace(str);

        private EstadoCaballo GetEstado(string estado, EstadoCaballo estadoActual)
        {
            if (Enum.TryParse<EstadoCaballo>(estado, out var estadoNuevo))
                return estadoNuevo;
            return estadoActual;
        }


        private void SincronizarHistorialMedico(Caballo caballo, List<HistorialMedicoDto> padecimientosDto)
        {
            // ---------------------------------------------------------
            // PASO 1: ELIMINAR 
            // ---------------------------------------------------------

            // Obtenemos los IDs que vienen del Front
            var idsEntrantes = padecimientosDto
                .Where(x => x.HistorialId != 0) 
                .Select(x => x.HistorialId)
                .ToList();

            var registrosParaBorrar = caballo.HistorialMedico
                .Where(h => !idsEntrantes.Contains(h.Id))
                .ToList();

            foreach (var registro in registrosParaBorrar)
            {
                caballo.HistorialMedico.Remove(registro);
            }

            // ---------------------------------------------------------
            // PASO 2: AGREGAR O ACTUALIZAR (Lo que viene en el DTO)
            // ---------------------------------------------------------
            foreach (var itemDto in padecimientosDto)
            {
                var historialExistente = caballo.HistorialMedico
                    .FirstOrDefault(h => h.Id == itemDto.HistorialId);

                if (historialExistente != null)
                {
                    // --- EDITAR ---
                    historialExistente.Observaciones = itemDto.Observaciones;
                    historialExistente.PadecimientoId = itemDto.PadecimientoId; 
                }
                else
                {
                    // --- AGREGAR ---
                    // Si no tiene ID o el ID no coincide con nada existente, es nuevo
                    caballo.HistorialMedico.Add(new HistorialMedico
                    {
                        PadecimientoId = itemDto.PadecimientoId,
                        Observaciones = itemDto.Observaciones
                    });
                }
            }
        }
    }
}