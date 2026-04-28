using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Microservicios.Alojamiento.DataAccess.Context
{
    public class AlojamientoDbContext : DbContext
    {
        private readonly ILogger<AlojamientoDbContext> _logger;

        public AlojamientoDbContext(
            DbContextOptions<AlojamientoDbContext> options,
            ILogger<AlojamientoDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        // --- MODULO 1: IDENTIDAD ---
        public DbSet<usuarios> Usuarios { get; set; } = null!;
        public DbSet<roles> Roles { get; set; } = null!;
        public DbSet<usuario_roles> UsuarioRoles { get; set; } = null!;
        public DbSet<clientes> Clientes { get; set; } = null!;
        public DbSet<administradores> Administradores { get; set; } = null!;

        // --- MODULO 2: CATALOGO ---
        public DbSet<alojamientos> Alojamientos { get; set; } = null!;
        public DbSet<habitaciones> Habitaciones { get; set; } = null!;
        public DbSet<alojamiento_habitacion> AlojamientoHabitaciones { get; set; } = null!;

        // --- MODULO 3: SERVICIOS ---
        public DbSet<servicios> Servicios { get; set; } = null!;
        public DbSet<servicios_alojamiento> ServiciosAlojamientos { get; set; } = null!;

        // --- MODULO 4: RESERVAS Y TRANSACCIONES ---
        public DbSet<metodos_pago> MetodosPago { get; set; } = null!;
        public DbSet<reservas> Reservas { get; set; } = null!;
        public DbSet<reserva_detalles> ReservaDetalles { get; set; } = null!;
        public DbSet<facturas> Facturas { get; set; } = null!;

        // --- MODULO 5: FEEDBACK Y AUDITORIA ---
        public DbSet<resenas> Resenas { get; set; } = null!;
        public DbSet<auditoria> Auditorias { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // 🔥 SOLUCIÓN DEFINITIVA PARA usuarios.fecha_creacion
            modelBuilder.Entity<usuarios>()
                .Property(u => u.fecha_creacion)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            // 🔥 Evita que EF intente actualizar este campo en PUT
            modelBuilder.Entity<usuarios>()
                .Property(u => u.fecha_creacion)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(
                    Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore
                );

            // En AlojamientoDbContext.cs, dentro de OnModelCreating
            modelBuilder.Entity<clientes>(entity =>
            {
                entity.Property(e => e.id)
                      .HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<administradores>(entity =>
            {
                entity.Property(e => e.id)
                      .HasDefaultValueSql("uuid_generate_v4()");
            });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                    ));
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDateTimesToUtc();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDateTimesToUtc()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                foreach (var property in entry.Properties)
                {
                    var type = property.Metadata.ClrType;

                    // 🟢 DateTime
                    if (type == typeof(DateTime) || type == typeof(DateTime?))
                    {
                        var value = property.CurrentValue as DateTime?;

                        if (value.HasValue)
                        {
                            if (value.Value.Kind == DateTimeKind.Unspecified)
                            {
                                _logger.LogError("❌ Problema en: {Entidad}.{Propiedad}",
                                    entry.Entity.GetType().Name,
                                    property.Metadata.Name);

                                property.CurrentValue = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                            }
                            else if (value.Value.Kind == DateTimeKind.Local)
                            {
                                property.CurrentValue = value.Value.ToUniversalTime();
                            }
                        }
                    }

                    // 🔵 DateTimeOffset
                    if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                    {
                        var value = property.CurrentValue as DateTimeOffset?;

                        if (value.HasValue)
                        {
                            property.CurrentValue = value.Value.ToUniversalTime();
                        }
                    }
                }
            }
        }
    }
}