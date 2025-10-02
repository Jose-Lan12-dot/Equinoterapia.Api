using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;

namespace APIproyectoEquinoterapia.Models;

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
    public DbSet<LoginRespuesta> loginrespuesta { get; set; }
    public DbSet<Formulario> formulario { get; set; }
    public DbSet<FormularioRespuesta> formularioRespuesta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI;Database=EquinoterapiaDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

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
                .HasMaxLength(20)
                .HasColumnName("estado");
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
            entity.HasKey(e => e.CitaId).HasName("PK__Cita__5AC1B05BF327E219");

            entity.ToTable("cita");

            entity.Property(e => e.CitaId).HasColumnName("cita_id");
            entity.Property(e => e.Asistio).HasColumnName("asistio");
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.SesionId).HasColumnName("sesion_id");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__Cita__paciente_i__46E78A0C");

            entity.HasOne(d => d.Sesion).WithMany(p => p.Cita)
                .HasForeignKey(d => d.SesionId)
                .HasConstraintName("FK__Cita__sesion_id__45F365D3");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__A1EF56E897854518");

            entity.ToTable("instructor");

            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.PacienteId).HasName("PK__Paciente__46FEF656E35EEE7A");

            entity.ToTable("paciente");

            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasConversion<string>();
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");
            entity.Property(e => e.ActaNacimiento).HasColumnName("acta_nacimiento");
            entity.Property(e => e.CertificadoMedico).HasColumnName("certificado_medico");
            entity.Property(e => e.ComprobanteDomicilio).HasColumnName("comprobante_domicilio");
            entity.Property(e => e.Curp).HasColumnName("curp");
            entity.Property(e => e.DiagnosticoMedico).HasColumnName("diagnostico_medico");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Fotografia).HasColumnName("fotografia");
            entity.Property(e => e.IneTutor).HasColumnName("ine_tutor");
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


           entity.HasOne(e => e.Formulario)
                .WithOne(e => e.Paciente)
                .HasForeignKey<Formulario>(e => e.PacienteId);

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

        OnModelCreatingPartial(modelBuilder);


        modelBuilder.Entity<Formulario>(entity =>
        {
            entity.ToTable("formulario");
            entity.HasKey(e => e.FormularioId);

            entity.Property(e => e.FormularioId)
                .HasColumnName("formulario_id");

            entity.Property(e => e.PacienteId)
                .HasColumnName("paciente_id");

            entity.Property(e => e.Fecha)
                .HasColumnName("fecha");

            // Relación uno a uno con Paciente
            entity.HasOne(e => e.Paciente)
                .WithOne(p => p.Formulario)
                .HasForeignKey<Formulario>(e => e.PacienteId);

            // Relación uno a muchos con FormularioRespuesta
            entity.HasMany(e => e.Respuestas)
                .WithOne(r => r.Formulario)
                .HasForeignKey(r => r.FormularioId);
        });

        modelBuilder.Entity<FormularioRespuesta>(entity =>
        {
            entity.ToTable("formulario_respuesta");
            entity.HasKey(e => e.FormularioRespuestaId);

            entity.Property(e => e.FormularioRespuestaId)
                .HasColumnName("formulario_respuesta_id");

            entity.Property(e => e.FormularioId)
                .HasColumnName("formulario_id");

            entity.Property(e => e.Consulta)
                .HasColumnName("consulta");

            entity.Property(e => e.Respuesta)
                .HasColumnName("respuesta");
        });


    modelBuilder.Entity<LoginRespuesta>().HasNoKey(); // No tiene clave primaria
    }


        


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
