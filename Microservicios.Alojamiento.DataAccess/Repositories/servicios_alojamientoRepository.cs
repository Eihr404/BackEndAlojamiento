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
    public class servicios_alojamientoRepository : RepositoryBase<servicios_alojamiento>, Iservicios_alojamientoRepository
    {
        public servicios_alojamientoRepository(AlojamientoDbContext context) : base(context) { }

        public async Task<IEnumerable<servicios_alojamiento>> GetByAlojamientoAsync(Guid alojamientoId)
        {
            return await _context.ServiciosAlojamientos
                .Include(sa => sa.servicio)
                .Where(sa => sa.alojamiento_id == alojamientoId && sa.esta_activo)
                .ToListAsync();
        }
    }
}
