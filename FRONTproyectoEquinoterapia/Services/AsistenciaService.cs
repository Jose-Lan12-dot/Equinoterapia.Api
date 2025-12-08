using System.Net.Http.Json;
using System.Text.Json;
using FRONTproyectoEquinoterapia.Models;



namespace FRONTproyectoEquinoterapia.Services
{

    public class AsistenciasService
    {
        private readonly HttpClient _http;

        public AsistenciasService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AllPacienteDto>> ObtenerPorSesionAsync(int sesionId)
        {
            // Obtener las asistencias de la sesión
            var asistencias = await GetAsistenciasPorSesion(sesionId);

            // Convertir a AllPacienteDto
            var pacientes = asistencias.Select(a => new AllPacienteDto
            {
                PacienteId = a.PacienteId,
                Nombre = a.NombrePaciente
            }).ToList();

            return pacientes;
        }


        public async Task<List<GetAsistenciaDto>> GetAsistenciasPorSesion(int sesionId)
        {
            var res = await _http.GetFromJsonAsync<List<GetAsistenciaDto>>(
                $"api/asistencias?sesionId={sesionId}"
            );

            return res ?? new List<GetAsistenciaDto>();
        }

        public async Task<(bool ok, string mensaje)> CreateAsistencias(AddAsistenciaDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/asistencias", dto);

            var content = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (!res.IsSuccessStatusCode)
            {
                return (false, content?["Mensaje"] ?? "Error");
            }

            return (true, content?["Mensaje"] ?? "Creado");
        }

        public async Task<(bool ok, string mensaje)> UpdateAsistencias(UpdateAsistenciaDto dto)
        {
            var res = await _http.PatchAsync("api/asistencias", JsonContent.Create(dto));

            var content = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (!res.IsSuccessStatusCode)
            {
                return (false, content?["Mensaje"] ?? "Error");
            }

            return (true, content?["Mensaje"] ?? "Actualizado");
        }

        public async Task<(bool ok, string mensaje)> DeleteAsistencias(List<int> asistenciaIds)
        {
            var res = await _http.SendAsync(
                new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("api/asistencias", UriKind.Relative),
                    Content = JsonContent.Create(asistenciaIds)
                }
            );

            var content = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (!res.IsSuccessStatusCode)
            {
                return (false, content?["Mensaje"] ?? "Error");
            }

            return (true, content?["Mensaje"] ?? "Eliminado");
        }
    }

}


