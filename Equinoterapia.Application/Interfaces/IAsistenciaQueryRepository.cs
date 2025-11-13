using Equinoterapia.Application.DTOs.Asistencias;

namespace Equinoterapia.Application.Interfaces
{
    public interface IAsistenciaQueryRepository
    {
        // Este método reemplazará a GetAsistencias + MapToDto + GetNombrePaciente
        Task<List<GetAsistenciaDto>> GetAsistenciasBySesionIdAsync(int sesionId);
    }
}