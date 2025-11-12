//using APIproyectoEquinoterapia.DTOs;
//using System.ComponentModel.DataAnnotations;

////public class UpdatePacienteDto
////{
////    public int PacienteId { get; set; }
////    public string? Nombre { get; set; }
////    public DateOnly? FechaNacimiento { get; set; }
////    public string? Tutor { get; set; }
////    public string? TelefonoTutor { get; set; }
////    public string? Estado { get; set; }
////    public string? Genero { get; set; }
////}

//public class GetAllPacienteDto
//{
//    public int PacienteId { get; set; }
//    public required string Nombre { get; set; }
//    public required string Tutor { get; set; }
//    public required string Estado { get; set; }
//    public required int NumeroFaltas { get; set; }
//    public required bool FormularioHecho { get; set; }
//    public required bool FaltanArchivos { get; set; }
//}


//public class AddPacienteDto
//{
//    [Required]
//    [MinLength(3)]
//    [RegularExpression(@"\S+")]
//    public string Nombre { get; set; } = string.Empty;
//    [Required]
//    public DateOnly FechaNacimiento { get; set; }
//    [Required]
//    [MinLength(3)]
//    [RegularExpression(@"\S+")]
//    public  string Tutor { get; set; } = string.Empty;
//    [Required]
//    [MinLength(3)]
//    [RegularExpression(@"\S+")]
//    public string TelefonoTutor { get; set; } = string.Empty;
//    [Required]
//    [MinLength(3)]
//    [RegularExpression(@"\S+")]
//    public string Genero { get; set; } = string.Empty;
//}

////public class AddPacienteConArchivosDto
////{
////    [Required]
////    public AddPacienteDto Paciente { get; set; } = new();
////    public List<UpdateArchivoDto>? Archivos { get; set; }
////}

