using FRONTproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FRONTproyectoEquinoterapia.Services
{
    // Wrapper genérico para leer { Message, Data } del backend
    public class ResponseWrapper<T>
    {
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
    }

    public class PacienteService
    {
        private readonly HttpClient _http;

        public PacienteService(HttpClient http)
        {
            _http = http;
        }

        // 🔹 Obtener todos los pacientes
        public async Task<List<AllPacienteDto>> ObtenerTodosAsync()
        {
            var response = await _http.GetFromJsonAsync<ResponseWrapper<List<AllPacienteDto>>>("https://localhost:7280/api/Pacientes");
            return response?.Data ?? new List<AllPacienteDto>();
        }

        // 🔹 Obtener detalle de un paciente por ID
        public async Task<PacienteDetailsDto?> ObtenerPorIdAsync(int pacienteId)
        {
            var response = await _http.GetFromJsonAsync<ResponseWrapper<PacienteDetailsDto>>(
                $"https://localhost:7280/api/Pacientes/{pacienteId}");

            // Retorna solo el objeto Data (PacienteDetailsDto)
            return response?.Data;
        }


        // 🔹 Agregar un nuevo paciente solo datos no archivos 
        public async Task<(bool exito, string mensaje, int pacienteId)> AgregarPacienteSoloDatosAsync(AddPacienteDto paciente)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7280/api/Pacientes", paciente);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return (false, err, 0);
            }

            var creado = await response.Content.ReadFromJsonAsync<PacienteCreadoDto>();
            return (true, "Paciente creado", creado!.PacienteId);
        }





        // 🔹 Agregar un nuevo paciente con archivos
        public async Task<(bool exito, string mensaje)> AgregarPacienteAsync(AddPacienteConArchivosDto pacienteConArchivos)
        {
            using var formData = new MultipartFormDataContent();

            // 🧩 Datos del paciente
            formData.Add(new StringContent(pacienteConArchivos.Paciente.Nombre), "Paciente.Nombre");
            formData.Add(new StringContent(pacienteConArchivos.Paciente.Tutor), "Paciente.Tutor");
            formData.Add(new StringContent(pacienteConArchivos.Paciente.TelefonoTutor), "Paciente.TelefonoTutor");
            formData.Add(new StringContent(pacienteConArchivos.Paciente.Genero), "Paciente.Genero");
            formData.Add(new StringContent(pacienteConArchivos.Paciente.FechaNacimiento.ToString("yyyy-MM-dd")), "Paciente.FechaNacimiento");

            // 🗂 Archivos opcionales
            void AddFile(string nombreCampo, IBrowserFile? archivo)
            {
                if (archivo != null)
                {
                    var streamContent = new StreamContent(archivo.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);
                    formData.Add(streamContent, nombreCampo, archivo.Name);
                }
            }

            AddFile("DiagnosticoMedico", pacienteConArchivos.DiagnosticoMedico);
            AddFile("CertificadoMedico", pacienteConArchivos.CertificadoMedico);
            AddFile("ActaNacimiento", pacienteConArchivos.ActaNacimiento);
            AddFile("Curp", pacienteConArchivos.Curp);
            AddFile("ComprobanteDomicilio", pacienteConArchivos.ComprobanteDomicilio);
            AddFile("IneTutor", pacienteConArchivos.IneTutor);
            AddFile("Fotografia", pacienteConArchivos.Fotografia);

            var response = await _http.PostAsync("https://localhost:7280/api/Pacientes", formData);

            if (response.IsSuccessStatusCode)
                return (true, "Paciente agregado correctamente.");

            string error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

        // 🔹 Actualizar completamente un paciente
        public async Task<(bool exito, string mensaje)> ActualizarPacienteAsync(UpdatePacienteDto paciente)
        {
            var response = await _http.PutAsJsonAsync(
                $"https://localhost:7280/api/Pacientes/{paciente.PacienteId}",
                paciente);

            if (response.IsSuccessStatusCode)
                return (true, "Paciente actualizado correctamente.");

            string error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

        // 🔹 Actualizar parcialmente un paciente
        public async Task<(bool exito, string mensaje)> ActualizarParcialAsync(PacienteDto paciente)
        {
            var response = await _http.PatchAsJsonAsync(
                $"https://localhost:7280/api/Pacientes/{paciente.PacienteId}",
                paciente);

            if (response.IsSuccessStatusCode)
                return (true, "Paciente actualizado parcialmente.");

            string error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

        // 🔹 Eliminar un paciente por ID
        public async Task<(bool exito, string mensaje)> EliminarPacienteAsync(int pacienteId)
        {
            var response = await _http.DeleteAsync($"https://localhost:7280/api/Pacientes/{pacienteId}");
            if (response.IsSuccessStatusCode)
                return (true, "Paciente eliminado correctamente.");

            string error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }

    }

    // 🔹 Clase auxiliar para la respuesta del endpoint obtener-por-id
    public class PacienteDetalleResponse
    {
        public PacienteDto? Paciente { get; set; }
        public int NumeroFaltas { get; set; }
        public bool FaltaEvaluacion { get; set; }
        public List<string>? ArchivosPendientes { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}

