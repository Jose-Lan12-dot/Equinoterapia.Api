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


        // Crear paciente solo con datos
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

        // 🔹 Agregar paciente con archivos (DTO único)
        public async Task<(bool exito, string mensaje)> AgregarPacienteAsync(AddPacienteConArchivosDto pacienteConArchivos)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "https://localhost:7280/api/Pacientes",
                    pacienteConArchivos
                );

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (false, error);
                }

                return (true, "Paciente creado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }



        // 🔹 Actualizar completamente un paciente
        public async Task<(bool exito, string mensaje)> ActualizarPacienteAsync(UpdatePacienteDto paciente)
        {
            var response = await _http.PatchAsJsonAsync(
                $"https://localhost:7280/api/Pacientes/{paciente.PacienteId}",
                paciente);

            if (response.IsSuccessStatusCode)
                return (true, "Paciente actualizado correctamente.");

            string error = await response.Content.ReadAsStringAsync();
            return (false, $"Error: {response.StatusCode} - {error}");
        }


        // 🔹 Actualizar parcialmente un paciente
        public async Task<(bool exito, string mensaje)> ActualizarParcialAsync(UpdatePacienteDto paciente)
        {
            var response = await _http.PatchAsJsonAsync(
                $"https://localhost:7280/api/Pacientes/{paciente.PacienteId}",
                paciente);

            if (response.IsSuccessStatusCode)
                return (true, "Paciente actualizado correctamente.");

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

