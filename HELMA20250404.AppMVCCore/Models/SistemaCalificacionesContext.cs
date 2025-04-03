using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HELMA20250404.AppMVCCore.Models;

public partial class SistemaCalificacionesContext : DbContext
{
    public SistemaCalificacionesContext()
    {
    }

    public SistemaCalificacionesContext(DbContextOptions<SistemaCalificacionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alumnos__3214EC077A20E37A");

            entity.HasIndex(e => e.IdUsuario, "UQ__Alumnos__5B65BF9656FCF5A6").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Encargado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nie)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NIE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false);

            // Configuración de la relación uno a uno con Usuario
            entity.HasOne(a => a.IdUsuarioNavigation) // Relación desde Alumno a Usuario
                  .WithOne(u => u.Alumno) // Relación inversa desde Usuario a Alumno
                  .HasForeignKey<Alumno>(a => a.IdUsuario) // Especifica la clave foránea en Alumno
                  .HasConstraintName("FK_Alumno_Usuario");
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aulas__3214EC07AE6FF075");

            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Materias__3214EC07E1D2FCAA");

            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("PK__Matricul__AD06C20FCC20DF0A");

            entity.ToTable("Matricula");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK__Matricula__IdAlu__4316F928");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdProfesor)
                .HasConstraintName("FK__Matricula__IdPro__440B1D61");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notas__3214EC07A9E7EC19");

            entity.Property(e => e.Estado)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when (([Trimestre1]+[Trimestre2])+[Trimestre3])/(3)>=(6) then 'APROBADO' else 'REPROBADO' end)", true);
            entity.Property(e => e.Promedio)
                .HasComputedColumnSql("((([Trimestre1]+[Trimestre2])+[Trimestre3])/(3))", true)
                .HasColumnType("decimal(11, 6)");
            entity.Property(e => e.Trimestre1).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Trimestre2).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Trimestre3).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdAula)
                .HasConstraintName("FK__Notas__IdAula__4BAC3F29");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("FK__Notas__IdMateria__4CA06362");

            entity.HasOne(d => d.IdMatriculaNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdMatricula)
                .HasConstraintName("FK__Notas__IdMatricu__4AB81AF0");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC07C762FA8C");

            entity.HasIndex(e => e.IdUsuario, "UQ__Profesor__5B65BF96942746B6").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Dui)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Profesore)
                .HasForeignKey<Profesore>(d => d.IdUsuario)
                .HasConstraintName("FK__Profesore__IdUsu__3C69FB99");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07A49E7ACA");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534B7C6D5AA").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);

            // Relación inversa con Alumno
            entity.HasOne(u => u.Alumno)
                  .WithOne(a => a.IdUsuarioNavigation) // Relación desde Usuario a Alumno
                  .HasForeignKey<Alumno>(a => a.IdUsuario) // Especifica la clave foránea en Alumno
                  .HasConstraintName("FK_Usuario_Alumno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    internal void Delete(Alumno alumnoData)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
