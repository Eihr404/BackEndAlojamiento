using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class reserva_detallesConfiguration : IEntityTypeConfiguration<reserva_detalles>
    {
        public void Configure(EntityTypeBuilder<reserva_detalles> builder)
        {
            builder.ToTable("reserva_detalles");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.tipo_item).HasConversion<string>().IsRequired();
            builder.Property(e => e.precio_capturado).HasPrecision(12, 2).IsRequired();
            builder.Property(e => e.cantidad).HasDefaultValue(1);

            builder.HasOne(e => e.reserva)
                   .WithMany(r => r.detalles)
                   .HasForeignKey(e => e.reserva_id);
        }
    }
}
