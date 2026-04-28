using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Queries;
using Microservicios.Alojamiento.DataAccess.Repositories;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{


    public interface IUnitOfWork : IDisposable
    {
        // Módulo Seguridad y Usuarios
        IusuarioRepository UsuarioRepository { get; }
        usuariosQueryRepository UsuarioQueryRepository { get; }

   

        IrolesRepository RolRepository { get; }
        rolesQueryRepository RolQueryRepository { get; }

        Iusuario_rolesRepository Usuario_rolesRepository { get; }
        usuario_rolesQueryRepository Usuario_rolesQueryRepository { get; }

        // Módulo Alojamiento y Estructura
        IAlojamientosRepository AlojamientosRepository { get; }
        alojamientosQueryRepository AlojamientosQueryRepository { get; }

        IhabitacionesRepository HabitacionesRepository { get; }
        habitacionesQueryRepository HabitacionesQueryRepository { get; }

        Ialojamiento_habitacionRepository Alojamiento_habitacionRepository { get; }
        alojamiento_habitacionQueryRepository Alojamiento_habitacionQueryRepository { get; }

        // Módulo Clientes y Administración
        IclientesRepository ClientesRepository { get; }
        clientesQueryRepository ClientesQueryRepository { get; }

        IadministradoresRepository AdministradoresRepository { get; }
        administradoresQueryRepository AdministradoresQueryRepository { get; }

        // Módulo Servicios
        IserviciosRepository ServiciosRepository { get; }
        serviciosQueryRepository ServiciosQueryRepository { get; }

        Iservicios_alojamientoRepository Servicios_alojamientoRepository { get; }
        servicios_alojamientoQueryRepository Servicios_alojamientoQueryRepository { get; }

        // Módulo Transaccional (Reservas y Facturación)
        IreservasRepository ReservasRepository { get; }
        reservasQueryRepository ReservasQueryRepository { get; }

        Ireserva_detallesRepository Reserva_detallesRepository { get; }
        reserva_detallesQueryRepository Reserva_detallesQueryRepository { get; }

        Imetodos_pagoRepository Metodos_pagoRepository { get; }
        metodos_pagoQueryRepository Metodos_pagoQueryRepository { get; }

        IfacturasRepository FacturasRepository { get; }
        facturasQueryRepository FacturasQueryRepository { get; }

        // Módulo Feedback y Auditoría
        IresenasRepository ResenasRepository { get; }
        resenasQueryRepository ResenasQueryRepository { get; }

        IauditoriaRepository AuditoriaRepository { get; }
        auditoriaQueryRepository AuditoriaQueryRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}