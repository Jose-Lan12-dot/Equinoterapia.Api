namespace FRONTproyectoEquinoterapia.Models
{

    public class InicioSesionModel
    {
        public string? usuario { get; set; }
        public string? contrasena { get; set; }
    }
    public class UsuarioSesionDto
    {
        public string Nombre { get; set; } = "";
        public string? FotoBase64 { get; set; }
    }
    public class UsuarioSesionService
    {
        public UsuarioSesionDto? Usuario { get; set; }
        public event Action? OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }


}
