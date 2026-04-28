using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class auditoriaConfiguration : IEntityTypeConfiguration<auditoria>
    {
        public void Configure(EntityTypeBuilder<auditoria> builder)
        {
            builder.ToTable("auditoria");
            builder.HasKey(e => e.id); // PK tipo Serial (int)

            builder.Property(e => e.accion).IsRequired().HasMaxLength(20);
            builder.Property(e => e.tabla_afectada).IsRequired().HasMaxLength(50);
            builder.Property(e => e.fecha_hora).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Mapeo de columna JSONB para PostgreSQL
            builder.Property(e => e.datos_anteriores)
                   .HasColumnType("jsonb");
        }
    }
}