using FRONTproyectoEquinoterapia.Models;
using Microsoft.JSInterop;

namespace FRONTproyectoEquinoterapia.Services
{
    public interface ISesionService
    {
        Task<(IEnumerable<SesionDto>, string)> ObtenerSesionesPorSemanaAsync(DateOnly startOfWeek);
        Task<(SesionDto?, string)> ObtenerSesionPorIdAsync(int sesionId); // <-- nuevo
        Task<(IEnumerable<SesionDto>, string)> ObtenerSesionesAsync(); // <-- nuevo
        Task<(bool, string)> AddAsync(AddSesionDto nuevaSesion);
        Task<(bool, string)> UpdateAsync(int sesionId, SesionDto sesionDto);
        Task<(bool, string)> DeleteAsync(int sesionId); // ya existente
        Task<bool> EliminarSesionAsync(int sesionId, IJSRuntime js); // <-- nuevo, solo para compatibilidad con el diálogo
        Task<bool> AgregarPacienteAsync(int sesionId, int pacienteId); // <-- si usas agregar pacientes
    }
}