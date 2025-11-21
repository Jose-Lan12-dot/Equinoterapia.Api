using Equinoterapia.Application.DTOs.Archivos;
using Equinoterapia.Domain.Entities;


namespace Equinoterapia.Application.Interfaces
{
    public interface IArchivoQueryRepository
    {
        Task<List<GetTipoArchivoDto>> GetTiposArchivos();
    }
}
