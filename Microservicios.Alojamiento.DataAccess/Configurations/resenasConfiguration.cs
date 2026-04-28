using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Configurations
{
    public class resenasConfiguration : IEntityTypeConfiguration<resenas>
    {
        public void Configure(EntityTypeBuilder<resenas> builder)
        {
            builder.ToTable("resenas"); // Mapeo exacto al script ASCII
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(e => e.estrellas).IsRequired();
            builder.Property(e => e.comentario).HasMaxLength(1000);
            builder.Property(e => e.fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relaciones
            builder.HasOne(e => e.cliente)
                   .WithMany(c => c.resenas)
                   .HasForeignKey(e => e.cliente_id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.alojamiento)
                   .WithMany(a => a.resenas)
                   .HasForeignKey(e => e.alojamiento_id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}