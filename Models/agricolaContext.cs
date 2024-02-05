using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgricolaProspectos.Models
{
    public partial class agricolaContext : DbContext
    {
        public agricolaContext()
        {
        }

        public agricolaContext(DbContextOptions<agricolaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documento> Documentos { get; set; } = null!;
        public virtual DbSet<ObservacionesRechazo> ObservacionesRechazos { get; set; } = null!;
        public virtual DbSet<Prospecto> Prospectos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>(entity =>
            {
                entity.ToTable("documentos");

                entity.Property(e => e.DocumentoId)
                    .ValueGeneratedNever()
                    .HasColumnName("documentoID");

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(255)
                    .HasColumnName("nombreDocumento");

                entity.Property(e => e.ProspectoId).HasColumnName("prospectoID");

                entity.Property(e => e.RutaDocumento)
                    .HasMaxLength(255)
                    .HasColumnName("rutaDocumento");

                entity.HasOne(d => d.Prospecto)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.ProspectoId)
                    .HasConstraintName("FK__documento__prosp__1367E606");
            });

            modelBuilder.Entity<ObservacionesRechazo>(entity =>
            {
                entity.HasKey(e => e.ObservacionId)
                    .HasName("PK__observac__A7E5A71A82B965C9");

                entity.ToTable("observacionesRechazo");

                entity.Property(e => e.ObservacionId)
                    .ValueGeneratedNever()
                    .HasColumnName("observacionID");

                entity.Property(e => e.Observacion).HasColumnName("observacion");

                entity.Property(e => e.ProspectoId).HasColumnName("prospectoID");

                entity.HasOne(d => d.Prospecto)
                    .WithMany(p => p.ObservacionesRechazos)
                    .HasForeignKey(d => d.ProspectoId)
                    .HasConstraintName("FK__observaci__prosp__164452B1");
            });

            modelBuilder.Entity<Prospecto>(entity =>
            {
                entity.ToTable("prospectos");

                entity.Property(e => e.ProspectoId)
                    .ValueGeneratedOnAdd()  
                    .HasColumnName("prospectoID");

                entity.Property(e => e.Calle)
                    .HasMaxLength(255)
                    .HasColumnName("calle");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(20)
                    .HasColumnName("codigoPostal");

                entity.Property(e => e.Colonia)
                    .HasMaxLength(255)
                    .HasColumnName("colonia");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(20)
                    .HasDefaultValue("Enviado")
                    .HasColumnName("estatus");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
                    .HasColumnName("numero");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(255)
                    .HasColumnName("primerApellido");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .HasColumnName("rfc");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(255)
                    .HasColumnName("segundoApellido");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
