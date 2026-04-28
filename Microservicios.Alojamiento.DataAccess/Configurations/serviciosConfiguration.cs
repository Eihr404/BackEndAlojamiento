using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class serviciosConfiguration : IEntityTypeConfiguration<servicios>
    {
        public void Configure(EntityTypeBuilder<servicios> builder)
        {
            builder.ToTable("servicios");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.nombre).IsRequired().HasMaxLength(100);
        }
    }
}
