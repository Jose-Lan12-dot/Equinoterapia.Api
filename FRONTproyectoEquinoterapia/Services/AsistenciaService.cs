using System.Net.Http.Json;
using System.Text.Json;
using FRONTproyectoEquinoterapia.Models;



namespace FRONTproyectoEquinoterapia.Services
{

    public class AsistenciasService
    {
        private readonly HttpClient _http;

        public AsistenciasService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("API"); // ✅ ESTE SÍ LLEVA TOKEN
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

            // Deserializamos en un DTO en lugar de diccionario
            var content = await res.Content.ReadFromJsonAsync<MensajeResponse>();
            string mensaje = content?.Mensaje ?? "Operación completada";

            return (res.IsSuccessStatusCode, mensaje);
        }

        public class MensajeResponse
        {
            public string Mensaje { get; set; } = string.Empty;
        }



        public async Task<(bool ok, string mensaje)> UpdateAsistencias(UpdateAsistenciaDto dto)
        {
            var response = await _http.PatchAsJsonAsync("api/asistencias", dto);

            if (!response.IsSuccessStatusCode)
            {
                // error — intentamos leer el json
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

                string msg = "";
                if (error != null && error.ContainsKey("Mensaje"))
                    msg = error["Mensaje"];

                return (false, msg != "" ? msg : "Error desconocido al actualizar asistencias.");
            }

            // éxito — leer json
            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            string mensaje = "";
            if (data != null && data.ContainsKey("Mensaje"))
                mensaje = data["Mensaje"];

            return (true, mensaje != "" ? mensaje : "Asistencias actualizadas correctamente.");
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

            Dictionary<string, string> content = null;
            if (res.Content.Headers.ContentLength > 0)
            {
                content = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            }

            if (!res.IsSuccessStatusCode)
            {
                return (false, content != null && content.ContainsKey("Mensaje") ? content["Mensaje"] : "Error desconocido");
            }

            return (true, content != null && content.ContainsKey("Mensaje") ? content["Mensaje"] : "Eliminado correctamente");
        }

    }

}


