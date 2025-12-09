using System.Net.Http.Json;
using System.Text.Json;
using FRONTproyectoEquinoterapia.Models;

namespace FRONTproyectoEquinoterapia.Services
{
        // EvaluacionService.cs
        public class EvaluacionService
        {
            private readonly HttpClient _http;

            public EvaluacionService(HttpClient http)
            {
                _http = http;
            }

            // Obtener evaluación completa de un paciente
            public async Task<GetEvaluacionDto?> GetEvaluacionAsync(int pacienteId)
            {
                var response = await _http.GetAsync($"api/pacientes/{pacienteId}/evaluacion");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadFromJsonAsync<JsonElement>();
                // dataEvaluacion está dentro del objeto
                if (content.TryGetProperty("dataEvaluacion", out var data))
                {
                    return JsonSerializer.Deserialize<GetEvaluacionDto>(data.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                return null;
            }

            // Obtener preguntas
            public async Task<List<PreguntasDto>> GetPreguntasAsync()
            {
                var response = await _http.GetAsync("api/preguntas");
                if (!response.IsSuccessStatusCode) return new List<PreguntasDto>();

                var preguntas = await response.Content.ReadFromJsonAsync<List<PreguntasDto>>();
                return preguntas ?? new List<PreguntasDto>();
            }

            // Crear evaluación
            public async Task<(bool exito, string mensaje)> CrearEvaluacionAsync(int pacienteId, AddEvaluacionDto evaluacionDto)
            {
                var response = await _http.PostAsJsonAsync($"api/pacientes/{pacienteId}/evaluacion", evaluacionDto);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    return (false, msg);
                }

                return (true, "Evaluación creada correctamente");
            }

            // Actualizar evaluación
            public async Task<(bool exito, string mensaje)> ActualizarEvaluacionAsync(int pacienteId, UpdateEvaluacionDto evaluacionDto)
            {
                var response = await _http.PatchAsJsonAsync($"api/pacientes/{pacienteId}/evaluacion", evaluacionDto);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    return (false, msg);
                }

                return (true, "Evaluación actualizada correctamente");
            }
        }
}
