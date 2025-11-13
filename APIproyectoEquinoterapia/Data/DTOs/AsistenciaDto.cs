//using System.ComponentModel.DataAnnotations;

//namespace APIproyectoEquinoterapia.DTOs
//{
//    public class GetAsistenciaDto
//    {
//        public int AsistenciaId { get; set; }
//        public string nombre { get; set; } = string.Empty;
//        public bool Asistio { get; set; }
//    }

//    public class AddAsistenciaDto
//    {
//        [Required]
//        public int SesionId { get; set; }
//        [Required]
//        [MinLength(1)]
//        public List<int> PacientesId { get; set; } = new ();
//    }

//    public class UpdateAsistenciaItemDto
//    {
//        [Required]
//        public int AsistenciasId { get; set; }
//        [Required]
//        public bool Asistio { get; set; }
//    }
//    public class UpdateAsistenciaDto
//    {
//        [Required]
//        [MinLength(1)]
//        public List<UpdateAsistenciaItemDto> Asistencias { get; set; } = new ();
//    }
//}
