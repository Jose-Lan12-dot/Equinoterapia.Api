using APIproyectoEquinoterapia.Data.Models.Archivo_;
using APIproyectoEquinoterapia.Data.Models.Evaluacion_;
using System;
using System.Collections.Generic;


namespace APIproyectoEquinoterapia.Data.Models
{
    public enum EstadoPaciente
    {
        Activo,
        Baja,
        TratamientoCompleto
    }

    public enum Generos
    {
        Masculino,
        Femenino,
        NoBinario
    }

    public partial class Paciente
    {
        public int PacienteId { get; set; }

        public string? Nombre { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Tutor { get; set; }

        public string? TelefonoTutor { get; set; }

        public EstadoPaciente Estado { get; set; }

        public Generos Genero { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
        public virtual ICollection<Archivos> Archivos { get; set; } = new List<Archivos>();
        public virtual Evaluacion? Evaluacion { get; set; }

    }

}