using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;


namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class rolesConfiguration : IEntityTypeConfiguration<roles>
    {
        public void Configure(EntityTypeBuilder<roles> builder)
        {
            builder.ToTable("roles");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.nombre).IsRequired().HasMaxLength(50);
        }
    }
}
