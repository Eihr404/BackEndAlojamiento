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
    public class facturasRepository : RepositoryBase<facturas>, IfacturasRepository
    {
        public facturasRepository(AlojamientoDbContext context) : base(context) { }

        public async Task<facturas?> GetByReservaIdAsync(Guid reservaId)
        {
            return await _context.Facturas
                .FirstOrDefaultAsync(f => f.reserva_id == reservaId);
        }
    }
}
