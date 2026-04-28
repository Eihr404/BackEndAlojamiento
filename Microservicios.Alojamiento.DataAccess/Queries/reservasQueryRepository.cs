using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class reservasQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public reservasQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<IEnumerable<reservas>> GetByClienteIdAsync(Guid clienteId)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Include(r => r.alojamiento) // Útil para mostrar el nombre del hotel en el historial
                .Where(r => r.cliente_id == clienteId)
                .OrderByDescending(r => r.fecha_solicitud)
                .ToListAsync();
        }
        public async Task<PagedResult<reservas>> GetHistorialPagedAsync(Guid usuarioId, bool esAdmin, int page, int size)
        {
            var query = _context.Reservas.AsNoTracking()
                .Include(r => r.alojamiento)
                .AsQueryable();

            query = esAdmin
                ? query.Where(r => r.alojamiento.admin_id == usuarioId)
                : query.Where(r => r.cliente_id == usuarioId);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size)
                             .OrderByDescending(r => r.fecha_solicitud).ToListAsync();

            return new PagedResult<reservas>(items, total, page, size);
        }

        public async Task<IEnumerable<reservas>> GetByAdminIdAsync(Guid adminId)
        {
            return await _context.Reservas.AsNoTracking()
                .Include(r => r.cliente)
                .Include(r => r.alojamiento)
                .Where(r => r.alojamiento.admin_id == adminId)
                .ToListAsync();
        }
        public async Task<reservas?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Include(r => r.alojamiento)
                .Include(r => r.detalles) // Cargamos detalles si existen
                .FirstOrDefaultAsync(r => r.id == id);
        }
    }
}
