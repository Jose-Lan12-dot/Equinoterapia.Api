namespace Equinoterapia2.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int FaltasAcumuladas { get; set; }
        public required string Estado { get; set; }
        public bool EvaluacionCompleta { get; set; }
    }
}

