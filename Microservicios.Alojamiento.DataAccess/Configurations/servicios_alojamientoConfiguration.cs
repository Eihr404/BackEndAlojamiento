using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class servicios_alojamientoConfiguration : IEntityTypeConfiguration<servicios_alojamiento>
    {
        public void Configure(EntityTypeBuilder<servicios_alojamiento> builder)
        {
            builder.ToTable("servicios_alojamiento");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.precio_adicional).HasPrecision(12, 2).IsRequired();
            builder.Property(e => e.esta_activo).HasDefaultValue(true);

            builder.HasOne(e => e.alojamiento)
                   .WithMany(a => a.servicios_ofertados)
                   .HasForeignKey(e => e.alojamiento_id);

            builder.HasOne(e => e.servicio)
                   .WithMany(s => s.alojamientos_que_lo_ofrecen)
                   .HasForeignKey(e => e.servicio_id);
        }
    }
}
