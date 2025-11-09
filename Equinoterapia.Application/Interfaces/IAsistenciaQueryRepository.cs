using Equinoterapia.Application.DTOs;

namespace Equinoterapia.Application.Interfaces
{
    public interface IAsistenciaQueryRepository
    {
        // Este método reemplazará a GetAsistencias + MapToDto + GetNombrePaciente
        Task<List<AsistenciaDto>> GetAsistenciasBySesionIdAsync(int sesionId);
    }
}