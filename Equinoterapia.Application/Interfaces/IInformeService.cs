using Equinoterapia.Application.DTOs.Informes;

namespace Equinoterapia.Application.Interfaces
{
    public interface IInformeService
    {
        Task<InformeDto> GetInformeAsync(int mes, int anio);

        Task<(bool, string)> GuardarInformeAsync(InformeDto dto);

        //Task<(bool,string)> DeteleInforme(int mes, int anio);

        Task<GetEstadisticasDto> GetEstadisticasAsync(int mes, int anio);
    }
}