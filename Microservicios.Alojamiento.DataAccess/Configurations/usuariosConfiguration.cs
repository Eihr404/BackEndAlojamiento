using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class usuariosConfiguration : IEntityTypeConfiguration<usuarios>
    {
        public void Configure(EntityTypeBuilder<usuarios> builder)
        {
            builder.ToTable("usuarios");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.email).IsRequired().HasMaxLength(150);
            builder.HasIndex(e => e.email).IsUnique();
            builder.Property(e => e.password_hash).IsRequired();
            builder.Property(e => e.fecha_creacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}