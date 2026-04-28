using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class reservasConfiguration : IEntityTypeConfiguration<reservas>
    {
        public void Configure(EntityTypeBuilder<reservas> builder)
        {
            builder.ToTable("reservas");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.estado).HasConversion<string>().IsRequired();
            builder.Property(e => e.monto_total).HasPrecision(12, 2).IsRequired();
            builder.Property(e => e.fecha_solicitud).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(e => e.cliente)
                   .WithMany(c => c.reservas)
                   .HasForeignKey(e => e.cliente_id);

            builder.HasOne(e => e.alojamiento)
                   .WithMany(a => a.reservas)
                   .HasForeignKey(e => e.alojamiento_id);
        }
    }
}
