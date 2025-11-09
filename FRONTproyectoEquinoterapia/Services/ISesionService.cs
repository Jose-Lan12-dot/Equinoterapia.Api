using FRONTproyectoEquinoterapia.Dtos;

namespace FRONTproyectoEquinoterapia.Services
{
    public interface ISesionService
    {
        // Para el calendario del Home
        Task<(IEnumerable<SesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek);

        // Para la página de "Editar Sesión"
        Task<(SesionDto?, string)> GetByIdAsync(int sesionId);

        // Para el formulario de "Agregar Sesión"
        Task<(bool, string)> AddAsync(AddSesionDto addSesionDto);

        // Para el formulario de "Editar Sesión"
        Task<(bool, string)> UpdateAsync(int sesionId, SesionDto sesionDto);

        // Para el botón de "Eliminar"
        Task<(bool, string)> DeleteAsync(int sesionId);
    }
}