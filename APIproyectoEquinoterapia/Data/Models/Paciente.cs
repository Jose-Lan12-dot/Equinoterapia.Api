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

        public required string Nombre { get; set; }

        public required DateOnly FechaNacimiento { get; set; }

        public required string Tutor { get; set; }

        public required string TelefonoTutor { get; set; }

        public required EstadoPaciente Estado { get; set; }

        public required Generos Genero { get; set; }

        public required DateTime FechaRegistro { get; set; }



        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
        public virtual ICollection<Archivos> Archivos { get; set; } = new List<Archivos>();
        public virtual Evaluacion? Evaluacion { get; set; }

    }

}