using FRONTproyectoEquinoterapia.Models;
using QuestPDF.Helpers;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;

namespace FRONTproyectoEquinoterapia.Services
{
    public interface IExportService
    {
        byte[] GenerarExcel(InformeDto informe);
        byte[] GenerarPdf(InformeDto informe);
    }

    public class ExportService : IExportService
    {
        public byte[] GenerarExcel(InformeDto informe)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Informe");

            // Cabeceras
            worksheet.Cell(1, 1).Value = "Número";
            worksheet.Cell(1, 2).Value = "Situación";
            worksheet.Cell(1, 3).Value = "Acciones";
            worksheet.Cell(1, 4).Value = "Avances";
            worksheet.Cell(1, 5).Value = "Trámite";
            worksheet.Cell(1, 6).Value = "Observaciones";

            int fila = 2;
            foreach (var detalle in informe.Detalles)
            {
                worksheet.Cell(fila, 1).Value = detalle.NumeroOrden;
                worksheet.Cell(fila, 2).Value = detalle.Situacion;
                worksheet.Cell(fila, 3).Value = detalle.Acciones;
                worksheet.Cell(fila, 4).Value = detalle.Avances;
                worksheet.Cell(fila, 5).Value = detalle.Tramite;
                worksheet.Cell(fila, 6).Value = detalle.Observaciones;
                fila++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GenerarPdf(InformeDto informe)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text($"Informe Mensual: {informe.Mes}/{informe.Anio}").Bold().FontSize(16);
                            col.Item().LineHorizontal(1);
                            foreach (var detalle in informe.Detalles)
                            {
                                col.Item().Text($"{detalle.NumeroOrden}. {detalle.Situacion} | {detalle.Acciones} | {detalle.Avances} | {detalle.Tramite} | {detalle.Observaciones}");
                            }
                        });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}

