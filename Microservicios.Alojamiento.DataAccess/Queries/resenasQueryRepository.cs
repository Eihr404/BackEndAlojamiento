using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class resenasQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public resenasQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<resenas?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Resenas
                .AsNoTracking()
                .Include(r => r.cliente) // Traemos al cliente para ver quién comentó
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task<PagedResult<resenas>> GetByAlojamientoPagedAsync(Guid alojamientoId, int page, int size)
        {
            var query = _context.Resenas.AsNoTracking()
                .Include(r => r.cliente)
                .Where(r => r.alojamiento_id == alojamientoId);

            var total = await query.CountAsync();
            var items = await query.OrderByDescending(r => r.fecha)
                             .Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedResult<resenas>(items, total, page, size);
        }
    }
}
