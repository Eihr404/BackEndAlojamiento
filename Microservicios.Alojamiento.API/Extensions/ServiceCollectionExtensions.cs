using FluentValidation;                                              // ✅ agregado
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Services;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Microservicios.Alojamiento.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<AlojamientoDbContext>(options =>
            options.UseNpgsql(connectionString));

        // ✅ Registra todos los validadores de FluentValidation del proyecto Business
        services.AddValidatorsFromAssemblyContaining<AdministradoresService>();

        // Registro de Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registro de Servicios
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientesService, ClientesService>();
        services.AddScoped<IAdministradoresService, AdministradoresService>();
        services.AddScoped<IAlojamientosService, AlojamientosService>();
        services.AddScoped<IHabitacionesService, HabitacionesService>();
        services.AddScoped<IReservasService, ReservasService>();
        services.AddScoped<IReserva_detallesService, Reserva_detallesService>();
        services.AddScoped<IFacturasService, FacturasService>();
        services.AddScoped<IResenasService, ResenasService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IServiciosService, ServiciosService>();
        services.AddScoped<IServicios_alojamientoService, Servicios_alojamientoService>();
        services.AddScoped<IUsuario_rolesService, Usuario_rolesService>();
        services.AddScoped<IUsuariosService, UsuariosService>();
        services.AddScoped<IAuditoriaService, AuditoriaService>();
        services.AddScoped<IAlojamiento_habitacionService, Alojamiento_habitacionService>();
        services.AddScoped<IMetodos_pagoService, Metodos_pagoService>();
    }
}