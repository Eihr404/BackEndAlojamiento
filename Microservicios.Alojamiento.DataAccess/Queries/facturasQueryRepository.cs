using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class facturasQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public async Task<facturas?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Facturas
                .Include(f => f.reserva)
                .Include(f => f.metodo_pago)
                .FirstOrDefaultAsync(f => f.id == id);
        }
        // Añade este método en facturasQueryRepository.cs
        public async Task<facturas?> ObtenerPorReservaAsync(Guid reservaId)
        {
            return await _context.Facturas
                .Include(f => f.reserva)
                .Include(f => f.metodo_pago)
                .FirstOrDefaultAsync(f => f.reserva_id == reservaId);
        }
        public facturasQueryRepository(AlojamientoDbContext context) => _context = context;
        public async Task<facturas?> GetFacturaConDetallesAsync(string numFactura) =>
            await _context.Facturas
                .Include(f => f.reserva).ThenInclude(r => r.detalles)
                .FirstOrDefaultAsync(f => f.num_factura == numFactura);
    }
}
