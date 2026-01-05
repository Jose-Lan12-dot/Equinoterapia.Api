using ClosedXML.Excel;
using Equinoterapia.Application.DTOs.Informes;
using Equinoterapia.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Equinoterapia.Infrastructure.Services
{
    public class ReportExportService : IInformeExportService
    {
        public ReportExportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // ==========================================
        // GENERACIÓN DE PDF 
        // ==========================================
        public byte[] GenerarPdf(InformeDto informe)
        {
            // 1. Cargar Imágenes
            var rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            var rutaLogo = Path.Combine(rutaBase, "Resources", "logoSecretaria.png");
            var rutaFondo = Path.Combine(rutaBase, "Resources", "logoSecretariaFondo.png");

            byte[] logoBytes = File.Exists(rutaLogo) ? File.ReadAllBytes(rutaLogo) : new byte[0];
            byte[] fondoBytes = File.Exists(rutaFondo) ? File.ReadAllBytes(rutaFondo) : new byte[0];

            
            var diasEnMes = DateTime.DaysInMonth(informe.Anio, informe.Mes);
            var nombreMes = new DateTime(informe.Anio, informe.Mes, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            
            nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape()); 
                    page.Margin(1, Unit.Centimetre); 
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                    // --- CAPA 1: FONDO (MARCA DE AGUA) ---
                    page.Content().Layers(layers =>
                    {
                        if (fondoBytes.Length > 0)
                        {
                            layers.Layer().AlignCenter().AlignMiddle().Image(fondoBytes).FitArea();
                        }

                        // --- CAPA 2: CONTENIDO ---
                        layers.PrimaryLayer().Column(col =>
                        {
                            // 1. ENCABEZADO SUPERIOR (Logos y Títulos)
                            col.Item().Row(row =>
                            {
                                // Izquierda: Logo
                                row.ConstantItem(4, Unit.Centimetre).Image(logoBytes).FitArea();


                                // Derecha: Coordinación (Rojo)
                                row.RelativeItem().AlignRight().AlignMiddle()
                                   .Text("CENTRO DE EQUINOTERRARIA")
                                   .FontSize(11).Bold().FontColor("#D9534F"); 
                            });

                            col.Item().PaddingVertical(10); // Espacio

                            // 2. DATOS DEL REPORTE (Tarjeta y Título)
                            col.Item().AlignRight()
                               .Text($"Tarjeta Informativa No. {informe.numeroTarjeta}")
                               .Bold().FontSize(11);

                            col.Item().PaddingBottom(5);

                            col.Item().AlignCenter()
                               .Text("INFORME DE ACCIONES DEL CENTRO DE EQUINOTERAPIA")
                               .Bold().FontSize(14);

                            col.Item().AlignCenter()
                               .Text($"01 al {diasEnMes} de {nombreMes} del {informe.Anio}")
                               .Bold().FontSize(12).Underline();

                            col.Item().PaddingVertical(10);

                            // 3. TABLA DE DATOS
                            col.Item().Table(table =>
                            {
                                // Definición de Columnas (ajustadas a ojo según imagen)
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(30);  // No.
                                    columns.RelativeColumn(2);   // Situaciones
                                    columns.RelativeColumn(2);   // Acciones
                                    columns.RelativeColumn(2);   // Avances
                                    columns.ConstantColumn(60);  // Tramite
                                    columns.RelativeColumn(2);   // Observaciones
                                });

                                // Encabezados
                                table.Header(header =>
                                {
                                    header.Cell().Element(EstiloEncabezado).Text("No.");
                                    header.Cell().Element(EstiloEncabezado).Text("SITUACIONES\n(Oportunidades)");
                                    header.Cell().Element(EstiloEncabezado).Text("ACCIONES");
                                    header.Cell().Element(EstiloEncabezado).Text("AVANCES");
                                    header.Cell().Element(EstiloEncabezado).Text("TRÁMITE");
                                    header.Cell().Element(EstiloEncabezado).Text("OBSERVACIONES");
                                });

                                // Filas
                                foreach (var item in informe.Detalles)
                                {
                                    table.Cell().Element(EstiloCelda).Text(item.NumeroOrden.ToString());
                                    table.Cell().Element(EstiloCelda).Text(item.Situacion);
                                    table.Cell().Element(EstiloCelda).Text(item.Acciones);
                                    table.Cell().Element(EstiloCelda).Text(item.Avances);
                                    table.Cell().Element(EstiloCelda).Text(item.Tramite);
                                    table.Cell().Element(EstiloCelda).Text(item.Observaciones);
                                }
                            });

                            
                            col.Item().PaddingTop(30).AlignCenter().Column(c =>
                            {
                                c.Item().Text("RESPETUOSAMENTE").Bold().FontSize(12);
                                
                            });
                        });
                    });

                    
                    page.Footer().AlignRight().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            return documento.GeneratePdf();
        }


        static IContainer EstiloEncabezado(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Black)
                .Background(Colors.White) 
                .Padding(3)
                .AlignCenter()
                .AlignMiddle()
                .DefaultTextStyle(x => x.Bold().FontSize(9));
        }

        static IContainer EstiloCelda(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Black)
                .Padding(3)
                .AlignLeft()
                .AlignTop()
                .DefaultTextStyle(x => x.FontSize(9));
        }


        // ==========================================
        // GENERACIÓN DE EXCEL 
        // ==========================================
        public byte[] GenerarExcel(InformeDto informe)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Informe");

            var rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            var rutaLogo = Path.Combine(rutaBase, "Resources", "logoSecretaria.png");

            var imagen = worksheet.AddPicture(rutaLogo)
              .MoveTo(worksheet.Cell("A1"))
              .Scale(0.1); 
            int filaInicio2 = 5;
         

            
            //worksheet.Cell("C2").Value = $"\"{informe.Anio}, Año de Rosario Castellanos Figueroa\"";
            worksheet.Range("C2:E2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell("C2").Style.Font.FontColor = XLColor.Gray;

            // Coordinación (Derecha)
            worksheet.Cell("F2").Value = "CENTRO DE EQUINOTERRARIA";
            worksheet.Cell("F2").Style.Font.FontColor = XLColor.Red; 
            worksheet.Cell("F2").Style.Font.Bold = true;
            worksheet.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            // Tarjeta Informativa
            worksheet.Cell("F4").Value = $"Tarjeta Informativa No. {informe.numeroTarjeta}";
            worksheet.Cell("F4").Style.Font.Bold = true;
            worksheet.Cell("F4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            // Título Principal
            worksheet.Cell("A5").Value = "INFORME DE ACCIONES DEL CENTRO DE EQUINOTERAPIA";
            worksheet.Range("A5:F5").Merge().Style.Font.Bold = true;
            worksheet.Range("A5:F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Fechas
            var diasEnMes = DateTime.DaysInMonth(informe.Anio, informe.Mes);
            var nombreMes = new DateTime(informe.Anio, informe.Mes, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);

            worksheet.Cell("A6").Value = $"01 al {diasEnMes} de {nombreMes} del {informe.Anio}";
            worksheet.Range("A6:F6").Merge().Style.Font.Bold = true;
            worksheet.Range("A6:F6").Style.Font.Underline = XLFontUnderlineValues.Single;
            worksheet.Range("A6:F6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // --- TABLA ---
            int filaInicio = 8;
            var headers = new[] { "No.", "SITUACIONES\n(Oportunidades)", "ACCIONES", "AVANCES", "TRÁMITE", "OBSERVACIONES" };

            for (int i = 0; i < headers.Length; i++)
            {
                var celda = worksheet.Cell(filaInicio, i + 1);
                celda.Value = headers[i];
                celda.Style.Font.Bold = true;
                celda.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                celda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                celda.Style.Alignment.WrapText = true; 
            }

            int row = filaInicio + 1;
            foreach (var detalle in informe.Detalles)
            {
                worksheet.Cell(row, 1).Value = detalle.NumeroOrden;
                worksheet.Cell(row, 2).Value = detalle.Situacion;
                worksheet.Cell(row, 3).Value = detalle.Acciones;
                worksheet.Cell(row, 4).Value = detalle.Avances;
                worksheet.Cell(row, 5).Value = detalle.Tramite;
                worksheet.Cell(row, 6).Value = detalle.Observaciones;

               
                for (int col = 1; col <= 6; col++)
                {
                    var celda = worksheet.Cell(row, col);
                    celda.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    celda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    celda.Style.Alignment.WrapText = true; // Texto largo se ajusta
                }
                row++;
            }


        // --- PIE (RESPETUOSAMENTE) ---
             row += 2; 
            worksheet.Range(row, 1, row, 6).Merge().Value = "RESPETUOSAMENTE";
            worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(row, 1).Style.Font.Bold = true;

            // Ajuste de anchos de columna (manual para parecerse a la imagen)
            worksheet.Column(1).Width = 5;  // No.
            worksheet.Column(2).Width = 30; // Situaciones
            worksheet.Column(3).Width = 30; // Acciones
            worksheet.Column(4).Width = 30; // Avances
            worksheet.Column(5).Width = 15; // Tramite
            worksheet.Column(6).Width = 25; // Observaciones

          
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.FitToPages(1, 0); 

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }


    }
}



