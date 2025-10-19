using APIproyectoEquinoterapia.Data.Models.Archivo_;
using APIproyectoEquinoterapia.Data.Models.Evaluacion_;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace APIproyectoEquinoterapia.Data.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caballo> caballo { get; set; }
    public virtual DbSet<Cita> cita { get; set; }
    public virtual DbSet<Instructor> instructor { get; set; }
    public virtual DbSet<Paciente> paciente { get; set; }
    public virtual DbSet<Sesion> sesion { get; set; }
    public DbSet<Evaluacion> evaluacion { get; set; }
    public DbSet<Respuestas> respuestas { get; set; }
    public DbSet<Preguntas> preguntas { get; set; }
    public DbSet<Archivos> archivo { get; set; }
    public DbSet<TipoArchivo> tipoArchivos { get; set; }
    public DbSet<InicioSesion> inicioSesion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caballo>(entity =>
        {
            entity.HasKey(e => e.Placa).HasName("PK__Caballo__0C057424DFD410E3");

            entity.ToTable("caballo");

            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("placa");
            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasConversion<string>();

            entity.Property(e => e.FechaHerraje).HasColumnName("fecha_herraje");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Padecimiento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("padecimientos");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(c => c.CitaId).HasName("PK__Cita__5AC1B05BF327E219");

            entity.ToTable("cita");

            entity.Property(c => c.CitaId).HasColumnName("cita_id");
            entity.Property(c => c.Asistio).HasColumnName("asistio");
            entity.Property(c => c.PacienteId).HasColumnName("paciente_id");
            entity.Property(c => c.SesionId).HasColumnName("sesion_id");

            entity.HasOne(c => c.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(c => c.PacienteId)
                .HasConstraintName("FK__Cita__paciente_i__46E78A0C");

            entity.HasOne(c => c.Sesion).WithMany(c => c.Cita)
                .HasForeignKey(d => d.SesionId)
                .HasConstraintName("FK__Cita__sesion_id__45F365D3");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(i => i.InstructorId).HasName("PK__Instruct__A1EF56E897854518");

            entity.ToTable("instructor");

            entity.Property(i => i.InstructorId).HasColumnName("instructor_id");
            entity.Property(i => i.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(i => i.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono");
            entity.Property(i => i.Estado)
                .HasConversion<string>()
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.PacienteId).HasName("PK__Paciente__46FEF656E35EEE7A");

            entity.ToTable("paciente");

            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasConversion<string>();
            entity.Property(e => e.Genero)
                .HasColumnName("genero")
                .HasConversion<string>();
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TelefonoTutor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono_tutor");
            entity.Property(e => e.Tutor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tutor");

        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.SesionId).HasName("PK__Sesion__0F8043C63FBA2D3F");

            entity.ToTable("sesion");

            entity.Property(e => e.SesionId).HasColumnName("sesion_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("placa");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__instruct__4222D4EF");

            entity.HasOne(d => d.PlacaNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.Placa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__placa__4316F928");
        });

        modelBuilder.Entity<Evaluacion>(entity =>
        {
            entity.ToTable("evaluacion");
            entity.HasKey(e => e.EvaluacionId);

            entity.Property(e => e.EvaluacionId)
                .HasColumnName("evaluacion_id");

            entity.Property(e => e.PacienteId)
                .HasColumnName("paciente_id");

            entity.Property(e => e.Fecha)
                .HasColumnName("fecha");

            // --- Relación 1:1 desde la entidad dependiente ---
            entity.HasOne(evaluacion => evaluacion.Paciente) 
                  .WithOne(paciente => paciente.Evaluacion)   
                  .HasForeignKey<Evaluacion>(evaluacion => evaluacion.PacienteId); 
        });

        modelBuilder.Entity<Respuestas>(entity =>
        {
            entity.ToTable("respuestas");
            entity.HasKey(r => r.RespuestaId);

            entity.Property(r => r.RespuestaId)
                .HasColumnName("respuesta_id");

            entity.Property(r => r.EvaluacionId)
                .HasColumnName("evaluacion_id");

            entity.Property(r => r.PreguntaId)
                .HasColumnName("pregunta_id");

            entity.Property(r => r.Respuesta)
                .HasColumnName("respuesta");

            // Contrato #1: Relación entre Respuesta y Evaluacion
            entity.HasOne(respuesta => respuesta.Evaluacion) 
                  .WithMany(evaluacion => evaluacion.Respuestas) 
                  .HasForeignKey(respuesta => respuesta.EvaluacionId); 

            // Contrato #2: Relación entre Respuesta y Pregunta
            entity.HasOne(respuesta => respuesta.Pregunta) 
                  .WithMany(pregunta => pregunta.Respuestas) 
                  .HasForeignKey(respuesta => respuesta.PreguntaId); 
        });

        modelBuilder.Entity<Preguntas>(entity =>
        {
            entity.ToTable("preguntas");
            entity.HasKey(p => p.PreguntaId);

            entity.Property(p => p.PreguntaId)
                .HasColumnName("pregunta_id");

            entity.Property(p => p.Pregunta)
                .HasColumnName("pregunta");
        });

        modelBuilder.Entity<Archivos>(entity =>
        {
            entity.ToTable("archivos");
            entity.HasKey(a => a.ArchivoId);

            entity.Property(a => a.ArchivoId)
                .HasColumnName("archivo_id");

            entity.Property(a => a.PacienteId)
                .HasColumnName("paciente_id");

            entity.Property(a => a.TipoArchivoId)
                .HasColumnName("tipo_archivo_id");

            entity.Property(a => a.Archivo).HasColumnName("archivo");

            // Contrato #1: Relación entre Arhchivo y Paciente
            entity.HasOne(archivo => archivo.Paciente)
                  .WithMany(paciente => paciente.Archivos)
                  .HasForeignKey(archivo => archivo.PacienteId);

            // Contrato #2: Relación entre Archivo y TipoArchivo
            entity.HasOne(archivo => archivo.TipoArchivo)
                  .WithMany(tipoArchivo => tipoArchivo.Archivos)
                  .HasForeignKey(archivo => archivo.TipoArchivoId);

        });


        modelBuilder.Entity<TipoArchivo>(entity =>
        {
            entity.ToTable("tipo_archivo");
            entity.HasKey(t => t.TipoArchivoId);

            entity.Property(t => t.TipoArchivoId)
                .HasColumnName("tipo_archivo_id");

            entity.Property(t => t.Nombre)
                .HasColumnName("nombre");

            entity.Property(t => t.Extension)
                .HasColumnName("extension");
        });

        modelBuilder.Entity<InicioSesion>().HasNoKey();


        modelBuilder.Entity<Preguntas>().HasData(
        new Preguntas { PreguntaId = 1, Pregunta = "CONTACTO VISUAL" },
        new Preguntas { PreguntaId = 2, Pregunta = "SECUENCIA DE INDICACIONES"},
        new Preguntas { PreguntaId = 3, Pregunta = "UTILIZA COMO INSTRUMENTO AL ADULTO"},
        new Preguntas { PreguntaId = 4, Pregunta = "CONDUCTA DE SEÑALADO PROIMPERATIVAS" +
                                                            " (PEDIR PARA EL)" },
        new Preguntas { PreguntaId = 5, Pregunta = "CONDUCTA DE SEÑALADO PROTODECLARATIVAS" +
                                                            " (FINALIDAD DE COMPARTIR)" },
        new Preguntas { PreguntaId = 6, Pregunta = "ATENCION CONJUNTA DE CUENTOS, JUEGOS O CANCIONES"},
        new Preguntas { PreguntaId = 7, Pregunta = "MOSTRAR INTERES POR COMPARTIR OBJETOS" },
        new Preguntas { PreguntaId = 8, Pregunta = "IMITACIÓN MOTORA GRUESA" },
        new Preguntas { PreguntaId = 9, Pregunta = "IMITACIÓN MOTORA FINA" },
        new Preguntas { PreguntaId = 10, Pregunta = "IMITACIÓN VOCAL"},
        new Preguntas { PreguntaId = 11, Pregunta = "PRESENCIA DE LENGUAJE ORAL Y NIVEL" },
        new Preguntas { PreguntaId = 12, Pregunta = "ECOLALIAL"},
        new Preguntas { PreguntaId = 13, Pregunta = "COMPRENSIÓN DE ORDENES SENCILLAS"},
        new Preguntas { PreguntaId = 14, Pregunta = "JUEGO DE FICCIÓN O SIMBÓLICO" },
        new Preguntas { PreguntaId = 15, Pregunta = "FLEXIBILIDAD A LOS CAMBIOS"},
        new Preguntas { PreguntaId = 16, Pregunta = "NIVEL DE ACTIVIDAD MOTORA"},
        new Preguntas { PreguntaId = 17, Pregunta = "NIVEL DE ATENCIÓN (TIEMPO DE ATENCIÓN REALIZANDO" +
                                                             " LA ACTIVIDAD)"},
        new Preguntas { PreguntaId = 18, Pregunta = "MOTIVACIÓN (INTERES RESTRINGIDOS)"},
        new Preguntas { PreguntaId = 19, Pregunta = "PLANIFICACIÓN"},
        new Preguntas { PreguntaId = 20, Pregunta = "NIVEL DE IMPULSIVIDAD"}
        );


        modelBuilder.Entity<TipoArchivo>().HasData(
            new TipoArchivo { TipoArchivoId = 1, Nombre = "DiagnosticoMedico", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 2, Nombre = "CertificadoMedico", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 3, Nombre = "ActaNacimiento", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 4, Nombre = "Curp", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 5, Nombre = "ComprobanteDomicilio", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 6, Nombre = "IneTutor", Extension = ".pdf" },
            new TipoArchivo { TipoArchivoId = 7, Nombre = "Fotografia", Extension = ".jpg" }
        );


    }



partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
