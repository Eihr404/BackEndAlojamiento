using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class rolesQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public rolesQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<roles?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.id == id);
        }
        public async Task<IEnumerable<roles>> GetAllRolesAsync() =>
            await _context.Roles.AsNoTracking().ToListAsync();
    }
}
