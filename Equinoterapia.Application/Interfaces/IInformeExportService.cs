using Equinoterapia.Application.DTOs.Informes;


namespace Equinoterapia.Application.Interfaces
{
    public interface IInformeExportService
    {
        byte[] GenerarPdf(InformeDto informe);
        byte[] GenerarExcel(InformeDto informe);
    }
}
