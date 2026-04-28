using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class clientesConfiguration : IEntityTypeConfiguration<clientes>
    {
        public void Configure(EntityTypeBuilder<clientes> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(e => e.nombre).IsRequired().HasMaxLength(100);
            builder.Property(e => e.apellido).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.usuario)
                   .WithOne(u => u.cliente)
                   .HasForeignKey<clientes>(e => e.usuario_id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
