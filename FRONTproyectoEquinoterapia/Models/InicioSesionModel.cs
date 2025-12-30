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

}
