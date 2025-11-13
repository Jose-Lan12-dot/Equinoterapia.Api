//using System.ComponentModel.DataAnnotations;

//namespace APIproyectoEquinoterapia.DTOs
//{
//    public class AddCaballoDto
//    {
//        [Required]
//        [StringLength(10, MinimumLength = 10)]
//        public string Placa { get; set; } = string.Empty;
//        [Required]
//        public string Nombre { get; set; } = string.Empty;
//        [Required]
//        public DateOnly? FechaHerraje { get; set; }
//        [Required]
//        public string Padecimiento { get; set; } = string.Empty;
//    }


//    public class UpdateCaballoDto
//    {
//        [Required]
//        public int CaballoId { get; set; }
//        [StringLength(10, MinimumLength = 10)]
//        public string? Placa { get; set; }
//        public string? Nombre { get; set; }
//        [MaxLength(15)]
//        public string? Estado { get; set; }
//        public DateOnly? FechaHerraje { get; set; }
//        public string? Padecimiento { get; set; }
//    }



//}
