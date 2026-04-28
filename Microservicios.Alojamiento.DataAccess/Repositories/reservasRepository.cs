using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class reservasRepository : RepositoryBase<reservas>, IreservasRepository
    {
        public reservasRepository(AlojamientoDbContext context) : base(context) { }

        public async Task<IEnumerable<reservas>> GetByClienteAsync(Guid clienteId)
        {
            return await _context.Reservas
                .Include(r => r.alojamiento)
                .Include(r => r.detalles)
                .Where(r => r.cliente_id == clienteId)
                .OrderByDescending(r => r.fecha_solicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<reservas>> GetByEstadoAsync(string estado)
        {
            return await _context.Reservas
                .Where(r => r.estado == estado)
                .ToListAsync();
        }
    }
}
