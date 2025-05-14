using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Models;


public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }



    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cita__3214EC07D600376F");

            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Motivo).HasMaxLength(250);

            entity.HasOne(d => d.Medico).WithMany(p => p.Cita)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__MedicoId__52593CB8");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__PacienteId__5165187F");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC07F45B3F2D");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medico__3214EC07EF434F64");

            entity.ToTable("Medico");

            entity.Property(e => e.Consultorio).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Especialidad).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.EspecialidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medico__Especial__4D94879B");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__3214EC07E225A9A4");

            entity.ToTable("Paciente");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Documento).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
