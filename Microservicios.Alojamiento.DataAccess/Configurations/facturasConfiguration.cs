using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class facturasConfiguration : IEntityTypeConfiguration<facturas>
    {
        public void Configure(EntityTypeBuilder<facturas> builder)
        {
            builder.ToTable("facturas");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.num_factura).IsRequired().HasMaxLength(20);
            builder.HasIndex(e => e.num_factura).IsUnique();
            builder.Property(e => e.estado_pago).HasConversion<string>();

            builder.HasOne(e => e.reserva)
                   .WithOne(r => r.factura)
                   .HasForeignKey<facturas>(e => e.reserva_id);

            builder.HasOne(e => e.metodo_pago)
                   .WithMany(m => m.facturas_asociadas)
                   .HasForeignKey(e => e.metodo_pago_id);
        }
    }
}
