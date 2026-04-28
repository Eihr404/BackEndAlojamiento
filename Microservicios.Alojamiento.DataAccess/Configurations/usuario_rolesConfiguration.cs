using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class usuario_rolesConfiguration : IEntityTypeConfiguration<usuario_roles>
    {
        public void Configure(EntityTypeBuilder<usuario_roles> builder)
        {
            builder.ToTable("usuario_roles");
            builder.HasKey(e => new { e.usuario_id, e.rol_id });

            builder.HasOne(e => e.usuario)
                   .WithMany(u => u.usuario_roles)
                   .HasForeignKey(e => e.usuario_id);

            builder.HasOne(e => e.rol)
                   .WithMany(r => r.usuario_roles)
                   .HasForeignKey(e => e.rol_id);
        }
    }
}
