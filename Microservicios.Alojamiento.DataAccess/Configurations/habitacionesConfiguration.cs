using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;


namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class habitacionesConfiguration : IEntityTypeConfiguration<habitaciones>
    {
        public void Configure(EntityTypeBuilder<habitaciones> builder)
        {
            builder.ToTable("habitaciones");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.nombre_tipo).IsRequired().HasMaxLength(100);
        }
    }
}
