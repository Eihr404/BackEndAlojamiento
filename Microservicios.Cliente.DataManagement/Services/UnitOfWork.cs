using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Queries;
using Microservicios.Alojamiento.DataAccess.Repositories;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using System;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlojamientoDbContext _context;

        // Propiedades de Repositorios y Queries
        public IusuarioRepository UsuarioRepository { get; }
        public usuariosQueryRepository UsuarioQueryRepository { get; }
        public IrolesRepository RolRepository { get; }
        public rolesQueryRepository RolQueryRepository { get; }
        public Iusuario_rolesRepository Usuario_rolesRepository { get; }
        public usuario_rolesQueryRepository Usuario_rolesQueryRepository { get; }

        public IAlojamientosRepository AlojamientosRepository { get; }
        public alojamientosQueryRepository AlojamientosQueryRepository { get; }
        public IhabitacionesRepository HabitacionesRepository { get; }
        public habitacionesQueryRepository HabitacionesQueryRepository { get; }
        public Ialojamiento_habitacionRepository Alojamiento_habitacionRepository { get; }
        public alojamiento_habitacionQueryRepository Alojamiento_habitacionQueryRepository { get; }

        public IclientesRepository ClientesRepository { get; }
        public clientesQueryRepository ClientesQueryRepository { get; }
        public IadministradoresRepository AdministradoresRepository { get; }
        public administradoresQueryRepository AdministradoresQueryRepository { get; }

        public IserviciosRepository ServiciosRepository { get; }
        public serviciosQueryRepository ServiciosQueryRepository { get; }
        public Iservicios_alojamientoRepository Servicios_alojamientoRepository { get; }
        public servicios_alojamientoQueryRepository Servicios_alojamientoQueryRepository { get; }

        public IreservasRepository ReservasRepository { get; }
        public reservasQueryRepository ReservasQueryRepository { get; }
        public Ireserva_detallesRepository Reserva_detallesRepository { get; }
        public reserva_detallesQueryRepository Reserva_detallesQueryRepository { get; }
        public Imetodos_pagoRepository Metodos_pagoRepository { get; }
        public metodos_pagoQueryRepository Metodos_pagoQueryRepository { get; }
        public IfacturasRepository FacturasRepository { get; }
        public facturasQueryRepository FacturasQueryRepository { get; }

        public IresenasRepository ResenasRepository { get; }
        public resenasQueryRepository ResenasQueryRepository { get; }
        public IauditoriaRepository AuditoriaRepository { get; }
        public auditoriaQueryRepository AuditoriaQueryRepository { get; }

        public UnitOfWork(AlojamientoDbContext context)
        {
            _context = context;

            // Inicialización de cada instancia
            UsuarioRepository = new usuariosRepository(_context);
            UsuarioQueryRepository = new usuariosQueryRepository(_context);
            RolRepository = new rolesRepository(_context);
            RolQueryRepository = new rolesQueryRepository(_context);
            Usuario_rolesRepository = new usuario_rolesRepository(_context);
            Usuario_rolesQueryRepository = new usuario_rolesQueryRepository(_context);

            AlojamientosRepository = new alojamientoRepository(_context);
            AlojamientosQueryRepository = new alojamientosQueryRepository(_context);
            HabitacionesRepository = new habitacionesRepository(_context);
            HabitacionesQueryRepository = new habitacionesQueryRepository(_context);
            Alojamiento_habitacionRepository = new alojamiento_habitacionRepository(_context);
            Alojamiento_habitacionQueryRepository = new alojamiento_habitacionQueryRepository(_context);

            ClientesRepository = new clientesRepository(_context);
            ClientesQueryRepository = new clientesQueryRepository(_context);
            AdministradoresRepository = new administradoresRepository(_context);
            AdministradoresQueryRepository = new administradoresQueryRepository(_context);

            ServiciosRepository = new serviciosRepository(_context);
            ServiciosQueryRepository = new serviciosQueryRepository(_context);
            Servicios_alojamientoRepository = new servicios_alojamientoRepository(_context);
            Servicios_alojamientoQueryRepository = new servicios_alojamientoQueryRepository(_context);

            ReservasRepository = new reservasRepository(_context);
            ReservasQueryRepository = new reservasQueryRepository(_context);
            Reserva_detallesRepository = new reserva_detallesRepository(_context);
            Reserva_detallesQueryRepository = new reserva_detallesQueryRepository(_context);
            Metodos_pagoRepository = new metodos_pagoRepository(_context);
            Metodos_pagoQueryRepository = new metodos_pagoQueryRepository(_context);
            FacturasRepository = new facturasRepository(_context);
            FacturasQueryRepository = new facturasQueryRepository(_context);

            ResenasRepository = new resenasRepository(_context);
            ResenasQueryRepository = new resenasQueryRepository(_context);
            AuditoriaRepository = new auditoriaRepository(_context);
            AuditoriaQueryRepository = new auditoriaQueryRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}