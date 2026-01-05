using Equinoterapia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Equinoterapia.Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Caballo> caballo { get; set; }
    public virtual DbSet<Asistencia> asistencia { get; set; }
    public virtual DbSet<Instructor> instructor { get; set; }
    public virtual DbSet<Paciente> paciente { get; set; }
    public virtual DbSet<Sesion> sesion { get; set; }
    public DbSet<Evaluacion> evaluacion { get; set; }
    public DbSet<Respuestas> respuestas { get; set; }
    public DbSet<Preguntas> preguntas { get; set; }
    public DbSet<Archivos> archivo { get; set; }
    public DbSet<TipoArchivo> tipoArchivo { get; set; }
    public DbSet<InicioSesion> inicioSesion { get; set; }
    public DbSet<InformeMensual> informeMensual { get; set; }
    public DbSet<DetalleInforme> DetalleInforme { get; set; }
    public DbSet<Padecimiento> padecimineto { get; set; }
    public DbSet<HistorialMedico> caballoPadecimiento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caballo>(entity =>
        {

            entity.ToTable("caballo");
            entity.HasKey(c => c.CaballoId);
            entity.Property(c => c.CaballoId)
                .HasColumnName("caballo_id");
            entity.Property(e => e.Placa)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasConversion<string>();
            entity.Property(e => e.FechaHerraje)
                .HasColumnName("fecha_herraje");
            entity.Property(e => e.Nombre)
                .IsUnicode(false)
                .HasColumnName("Nombre");
        });

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.ToTable("asistencia");
            entity.HasKey(a => a.AsistenciaId);

            entity.Property(a => a.AsistenciaId)
            .HasColumnName("asistencia_id");
            entity.Property(a => a.PacienteId)
            .HasColumnName("paciente_id");
            entity.Property(a => a.SesionId).
            HasColumnName("sesion_id");
            entity.Property(a => a.Asistio)
            .HasColumnName("asistio");



            entity.HasOne(asistencia => asistencia.Paciente)
                .WithMany(paciente => paciente.Asistencia)
                .HasForeignKey(asistencia => asistencia.PacienteId);

            entity.HasOne(asistencia => asistencia.Sesion)
                .WithMany(sesion => sesion.Asistencia)
                .HasForeignKey(asistencia => asistencia.SesionId);
        });

        modelBuilder.Entity<Instructor>(entity =>
        {

            entity.ToTable("instructor");
            entity.HasKey(i => i.InstructorId);

            entity.Property(i => i.InstructorId).HasColumnName("instructor_id");
            entity.Property(i => i.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre");
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
                .HasColumnName("Nombre");
            entity.Property(e => e.TelefonoTutor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono_tutor");
            entity.Property(e => e.Tutor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tutor");

            entity.Property(e => e.FechaUltimoReingreso)
                .HasColumnName("fecha_ultimo_registro");

        });

        modelBuilder.Entity<Sesion>(entity =>
        {

            entity.ToTable("sesion");
            entity.HasKey(e => e.SesionId);
            entity.Property(e => e.SesionId)
                .HasColumnName("sesion_id");
            entity.Property(e => e.Fecha)
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.InstructorId)
                .HasColumnName("instructor_id");
            entity.Property(e => e.CaballoId)
                .HasColumnName("caballo_id");

            // Contrato #2: Relación entre Sesion e Instructor
            entity.HasOne(sesion => sesion.Instructor)
                .WithMany(instructor => instructor.Sesions)
                .HasForeignKey(sesion => sesion.InstructorId);

            // Contrato #2: Relación entre Sesion y caballo
            entity.HasOne(sesion => sesion.Caballo)
            .WithMany(caballo => caballo.Sesions)
            .HasForeignKey(sesion => sesion.CaballoId);

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
                  .WithMany(paciente => paciente.Evaluacion)   
                  .HasForeignKey(evaluacion => evaluacion.PacienteId); 
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

            entity.Property(a => a.TipoId)
                .HasColumnName("tipo_id");

            entity.Property(a => a.Archivo).HasColumnName("archivo");

            // Contrato #1: Relación entre Arhchivo y Paciente
            entity.HasOne(archivo => archivo.Paciente)
                  .WithMany(paciente => paciente.Archivos)
                  .HasForeignKey(archivo => archivo.PacienteId);

            // Contrato #2: Relación entre Archivo y TipoArchivo
            entity.HasOne(archivo => archivo.TiposArchivo)
                  .WithMany(tipoArchivo => tipoArchivo.Archivos)
                  .HasForeignKey(archivo => archivo.TipoId);

        });


        modelBuilder.Entity<TipoArchivo>(entity =>
        {
            entity.ToTable("tipos_archivos");
            entity.HasKey(t => t.TipoId);

            entity.Property(t => t.TipoId)
                .HasColumnName("tipo_id");

            entity.Property(t => t.Nombre)
                .HasColumnName("Nombre");

            entity.Property(t => t.Extension)
                .HasColumnName("extension");
        });

        //modelBuilder.Entity<InicioSesion>().HasNoKey();
        modelBuilder.Entity<InicioSesion>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.Foto).HasColumnType("image");
        });

        modelBuilder.Entity<InformeMensual>(entity =>
        {
            entity.ToTable("informe_mensual");
            entity.HasKey(im => im.InformeMensualId);

            entity.Property(im => im.InformeMensualId)
                .HasColumnName("informe_mensual_id");

            entity.Property(im => im.Mes)
                .HasColumnName("mes");

            entity.Property(im => im.Anio)
                .HasColumnName("anio");

            entity.Property(im => im.numeroTarjeta)
                .HasColumnName("numero_tarjeta");

            entity.Property(im => im.FechaModificacion)
                .HasColumnName("fecha_modificaccion");

        });

        modelBuilder.Entity<DetalleInforme>(entity =>
        {
            entity.ToTable("detalle_informe");
            entity.HasKey(d => d.DetalleInformeId);

            entity.Property(d => d.DetalleInformeId)
                .HasColumnName("detalle_informe_id");

            entity.Property(d => d.InformeMensualId)
                .HasColumnName("informe_mensual_id");

            entity.Property(d => d.Situacion)
                .HasColumnName("situacion");

            entity.Property(d => d.Acciones)
                .HasColumnName("acciones");

            entity.Property(d => d.Avances)
                .HasColumnName("avances");

            entity.Property(d => d.Tramite)
                .HasColumnName("tramite");

            entity.Property(d => d.Observaciones)
                .HasColumnName("observaciones");

            // Contrato #1: Relación entre informeMensual y DetalleInforme
            entity.HasOne(detalles => detalles.Informe)
                  .WithMany(informe => informe.Detalles)
                  .HasForeignKey(detalles => detalles.InformeMensualId);

        });

        modelBuilder.Entity<Padecimiento>(entity =>
        {
            entity.ToTable("padecimiento");
            entity.HasKey(p => p.PadecimientoId);

            entity.Property(p => p.PadecimientoId)
                .HasColumnName("padecimiento_id");
            entity.Property(p => p.PadecimientoNombre)
                .HasColumnName("padecimiento_nombre");
        });

        modelBuilder.Entity<HistorialMedico>(entity =>
        {
            entity.ToTable("historial_medico");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("id");
            entity.Property(p => p.PadecimientoId)
                .HasColumnName("padecimiento_id");
            entity.Property(p => p.CaballoId)
                .HasColumnName("caballo_id");
            entity.Property(p => p.Observaciones)
                .HasColumnName("observaciones");

            entity.HasOne(hm => hm.Caballo)
            .WithMany(caballo => caballo.HistorialMedico)
            .HasForeignKey(hm => hm.CaballoId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(hm => hm.Padecimiento)
            .WithMany() 
            .HasForeignKey(hm => hm.PadecimientoId)
            .OnDelete(DeleteBehavior.Restrict);
        });




        ///INTRODUCIR DATOS A LA BASE DE DATOS
        modelBuilder.Entity<Padecimiento>().HasData(
        new Padecimiento { PadecimientoId = 3, PadecimientoNombre = "Artritis" },
        new Padecimiento { PadecimientoId = 4, PadecimientoNombre = "Infección Respiratoria" }
        );

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
            new TipoArchivo { TipoId = 1, Nombre = "DiagnosticoMedico", Extension = ".pdf" },
            new TipoArchivo { TipoId = 2, Nombre = "CertificadoMedico", Extension = ".pdf" },
            new TipoArchivo { TipoId = 3, Nombre = "ActaNacimiento", Extension = ".pdf" },
            new TipoArchivo { TipoId = 4, Nombre = "Curp", Extension = ".pdf" },
            new TipoArchivo { TipoId = 5, Nombre = "ComprobanteDomicilio", Extension = ".pdf" },
            new TipoArchivo { TipoId = 6, Nombre = "IneTutor", Extension = ".pdf" },
            new TipoArchivo { TipoId = 7, Nombre = "Fotografia", Extension = ".jpg" }
        );


    }



partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
