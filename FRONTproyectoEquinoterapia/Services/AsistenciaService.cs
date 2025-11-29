using System.Net.Http.Json;
using System.Text.Json;
using FRONTproyectoEquinoterapia.Models;

namespace FRONTproyectoEquinoterapia.Services
{
    public class AsistenciaService
    {
        private readonly HttpClient _http;

        public AsistenciaService(HttpClient http)
        {
            _http = http;
        }
        // Obtener asistencias (pacientes) de una sesión
        public async Task<List<PacienteDto>> ObtenerPacientesPorSesionAsync(int sesionId)
        {
            try
            {
                var response = await _http.GetAsync($"https://localhost:7280/api/asistencias?sesionId={sesionId}");

                if (!response.IsSuccessStatusCode) return new List<PacienteDto>();

                var json = await response.Content.ReadAsStringAsync();
                var obj = System.Text.Json.JsonSerializer.Deserialize<AsistenciasResponse>(json,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return obj?.Asistencias.Select(a => new PacienteDto
                {
                    PacienteId = a.AsistenciaId,
                    Nombre = a.Nombre // en tu backend es "nombre"
                }).ToList() ?? new List<PacienteDto>();
            }
            catch
            {
                return new List<PacienteDto>();
            }
        }


        public class AsistenciasResponse
        {
            public string Mensaje { get; set; }
            public List<AsistenciaDto> Asistencias { get; set; }
        }


      

        public async Task<bool> AgregarPacientesAsync(AsistenciaAddDto nueva)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7280/api/asistencia/agregar", nueva);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(AsistenciaUpdateDto updateDto)
        {
            var response = await _http.PutAsJsonAsync("https://localhost:7280/api/asistencia/actualizar", updateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(List<int> asistenciasIds)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:7280/api/asistencia/eliminar")
            {
                Content = JsonContent.Create(asistenciasIds)
            };
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}


