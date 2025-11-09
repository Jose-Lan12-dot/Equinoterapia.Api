using Radzen;
using System.Net.Http.Json;
using FrontEquinoterapia.Data.Models; // El using a tus DTOs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FRONTproyectoEquinoterapia.Dtos;

namespace FRONTproyectoEquinoterapia.Services
{
    public class SesionService : ISesionService
    {
        private readonly HttpClient _http;
        private readonly NotificationService _notificationService;

        // Asumiré que tus rutas de API son "api/sesion/..."
        // ¡Cámbialo si tu controlador se llama diferente!
        private const string ApiBasePath = "api/sesion";

        public SesionService(HttpClient http, NotificationService notificationService)
        {
            _http = http;
            _notificationService = notificationService;
        }

        public async Task<(IEnumerable<SesionDto>, string)> GetByWeekAsync(DateOnly startOfWeek)
        {
            // Formatea la fecha como espera tu API (ej: 2025-10-30)
            string formattedDate = startOfWeek.ToString("yyyy-MM-dd");

            try
            {
                // Llama a la API
                var response = await _http.GetAsync($"{ApiBasePath}/semana/{formattedDate}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<IEnumerable<SesionDto>>();
                    return (data ?? new List<SesionDto>(), "Datos cargados");
                }

                // Si la API devuelve un error (ej: 404, 500)
                var mensajeError = await response.Content.ReadAsStringAsync();
                MostrarError(mensajeError);
                return (new List<SesionDto>(), mensajeError);
            }
            catch (Exception ex)
            {
                // Si hay un error de red o de HttpClient
                MostrarError($"Error de conexión: {ex.Message}");
                return (new List<SesionDto>(), ex.Message);
            }
        }

        public async Task<(SesionDto?, string)> GetByIdAsync(int sesionId)
        {
            try
            {
                var response = await _http.GetAsync($"{ApiBasePath}/{sesionId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<SesionDto>();
                    return (data, "Datos cargados");
                }

                var mensajeError = await response.Content.ReadAsStringAsync();
                MostrarError(mensajeError);
                return (null, mensajeError);
            }
            catch (Exception ex)
            {
                MostrarError($"Error de conexión: {ex.Message}");
                return (null, ex.Message);
            }
        }

        public async Task<(bool, string)> AddAsync(AddSesionDto addSesionDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(ApiBasePath, addSesionDto);
                var mensaje = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MostrarExito(mensaje); // Muestra el mensaje de éxito del backend
                    return (true, mensaje);
                }

                MostrarError(mensaje); // Muestra el mensaje de error del backend
                return (false, mensaje);
            }
            catch (Exception ex)
            {
                MostrarError($"Error de conexión: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> UpdateAsync(int sesionId, SesionDto sesionDto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"{ApiBasePath}/{sesionId}", sesionDto);
                var mensaje = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MostrarExito(mensaje);
                    return (true, mensaje);
                }

                MostrarError(mensaje);
                return (false, mensaje);
            }
            catch (Exception ex)
            {
                MostrarError($"Error de conexión: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> DeleteAsync(int sesionId)
        {
            try
            {
                var response = await _http.DeleteAsync($"{ApiBasePath}/{sesionId}");
                var mensaje = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MostrarExito(mensaje);
                    return (true, mensaje);
                }

                MostrarError(mensaje);
                return (false, mensaje);
            }
            catch (Exception ex)
            {
                MostrarError($"Error de conexión: {ex.Message}");
                return (false, ex.Message);
            }
        }

        // --- Métodos Privados para Notificaciones ---
        private void MostrarError(string mensaje)
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = string.IsNullOrWhiteSpace(mensaje) ? "Ocurrió un error desconocido" : mensaje,
                Duration = 4000
            });
        }

        private void MostrarExito(string mensaje)
        {
            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Éxito",
                Detail = mensaje,
                Duration = 3000
            });
        }
    }
}