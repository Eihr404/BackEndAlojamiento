using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class auditoriaQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public auditoriaQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<IEnumerable<auditoria>> GetLogsByTableAsync(string tabla)
        {
            return await _context.Auditorias
                .AsNoTracking()
                .Where(a => a.tabla_afectada == tabla)
                .OrderByDescending(a => a.fecha_hora)
                .ToListAsync();
        }
        public async Task<auditoria?> ObtenerPorIdAsync(int id)
        {
            return await _context.Auditorias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.id == id);
        }

        public async Task<PagedResult<auditoria>> GetLogsPagedAsync(int page, int size)
        {
            var query = _context.Auditorias.AsNoTracking();
            var total = await query.CountAsync();
            var items = await query.OrderByDescending(a => a.fecha_hora)
                             .Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedResult<auditoria>(items, total, page, size);
        }
    }
}
