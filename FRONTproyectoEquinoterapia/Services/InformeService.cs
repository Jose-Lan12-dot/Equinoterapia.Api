using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FRONTproyectoEquinoterapia.Models;
using FRONTproyectoEquinoterapia.Services;
using BlazorDownloadFile;

namespace FRONTproyectoEquinoterapia.Services
{
    public class InformeService
    {
        private readonly HttpClient _httpClient;
        private readonly IExportService _exportService;
        private readonly IBlazorDownloadFileService _downloadService;

        public InformeService(
            IHttpClientFactory factory,
            IExportService exportService,
            IBlazorDownloadFileService downloadService)
        {
            _httpClient = factory.CreateClient("API"); // el handler ya agrega el token
            _exportService = exportService;
            _downloadService = downloadService;
        }

        // Obtener informe
        public async Task<InformeDto?> GetInformeAsync(int mes, int anio)
        {
            var response = await _httpClient.GetAsync($"api/Informes?mes={mes}&anio={anio}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<InformeDto>();
            return null;
        }

        // Obtener estadísticas
        public async Task<GetEstadisticasDto?> GetEstadisticasAsync(int mes, int anio)
        {
            var response = await _httpClient.GetAsync($"api/Informes/estadisticas?mes={mes}&anio={anio}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<GetEstadisticasDto>();
            return null;
        }

        // Guardar informe
        public async Task<(bool exito, string mensaje)> GuardarInformeAsync(InformeDto informe)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Informes", informe);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                return (true, result?.Mensaje ?? "Guardado correctamente.");
            }
            else
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                return (false, result?.Mensaje ?? "Error al guardar.");
            }
        }

        // Descargar PDF
        public async Task DescargarPdfAsync(int mes, int anio, string nombreArchivo)
        {
            var response = await _httpClient.GetAsync($"/api/Informes/download/pdf?mes={mes}&anio={anio}");
            response.EnsureSuccessStatusCode();

            var contenido = await response.Content.ReadAsByteArrayAsync();
            await _downloadService.DownloadFile(nombreArchivo, contenido, "application/pdf");
        }

        // Descargar Excel
        public async Task DescargarExcelAsync(int mes, int anio, string nombreArchivo)
        {
            var response = await _httpClient.GetAsync($"/api/Informes/download/excel?mes={mes}&anio={anio}");
            response.EnsureSuccessStatusCode();

            var contenido = await response.Content.ReadAsByteArrayAsync();
            await _downloadService.DownloadFile(
                nombreArchivo,
                contenido,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }

    // Clase auxiliar para leer mensajes del backend
    public class ResponseMessage
    {
        public string Mensaje { get; set; } = string.Empty;
    }
}
