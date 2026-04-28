using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class administradoresConfiguration : IEntityTypeConfiguration<administradores>
    {
        public void Configure(EntityTypeBuilder<administradores> builder)
        {
            builder.ToTable("administradores");
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.HasOne(e => e.usuario)
                   .WithOne(u => u.administrador)
                   .HasForeignKey<administradores>(e => e.usuario_id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
