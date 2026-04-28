using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class alojamiento_habitacionConfiguration : IEntityTypeConfiguration<alojamiento_habitacion>
    {
        public void Configure(EntityTypeBuilder<alojamiento_habitacion> builder)
        {
            builder.ToTable("alojamiento_habitacion");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.precio_noche).HasPrecision(12, 2).IsRequired();
            builder.Property(e => e.cantidad_total).IsRequired();

            // Relaciones
            builder.HasOne(e => e.alojamiento)
                   .WithMany(a => a.habitaciones_configuradas)
                   .HasForeignKey(e => e.alojamiento_id);

            builder.HasOne(e => e.habitacion)
                   .WithMany(h => h.alojamientos_vinculados)
                   .HasForeignKey(e => e.habitacion_id);
        }
    }
}
