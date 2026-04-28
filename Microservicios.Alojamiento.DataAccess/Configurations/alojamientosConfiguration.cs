using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class alojamientosConfiguration : IEntityTypeConfiguration<alojamientos>
    {
        public void Configure(EntityTypeBuilder<alojamientos> builder)
        {
            builder.ToTable("alojamientos");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.nombre).IsRequired().HasMaxLength(150);

            // Configuración del ENUM (Postgres)
            builder.Property(e => e.tipo)
                   .HasConversion<string>() // Lo guardamos como string o usamos Npgsql para mapear al tipo nativo
                   .IsRequired();

            builder.Property(e => e.ciudad).IsRequired().HasMaxLength(100);
            builder.Property(e => e.direccion).IsRequired().HasMaxLength(255);

            // Precisión para coordenadas y calificaciones
            builder.Property(e => e.latitud).HasPrecision(10, 8);
            builder.Property(e => e.longitud).HasPrecision(11, 8);
            builder.Property(e => e.calificacion_avg).HasPrecision(2, 1).HasDefaultValue(0.0m);

            // Mapeo de TIME a TimeSpan
            builder.Property(e => e.check_in).IsRequired();
            builder.Property(e => e.check_out).IsRequired();

            // Relación con Administrador
            builder.HasOne(e => e.administrador)
                   .WithMany(a => a.alojamientos)
                   .HasForeignKey(e => e.admin_id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}