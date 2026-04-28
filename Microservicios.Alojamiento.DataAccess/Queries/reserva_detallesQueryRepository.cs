using Microsoft.EntityFrameworkCore;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class reserva_detallesQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public reserva_detallesQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<reserva_detalles?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.ReservaDetalles
                .AsNoTracking()
                .FirstOrDefaultAsync(rd => rd.id == id);
        }

        public async Task<IEnumerable<reserva_detalles>> GetByReservaIdAsync(Guid reservaId)
        {
            return await _context.ReservaDetalles
                .AsNoTracking()
                .Where(rd => rd.reserva_id == reservaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<reserva_detalles>> GetDetallesByReservaAsync(Guid reservaId) =>
            await _context.ReservaDetalles.AsNoTracking()
                .Where(rd => rd.reserva_id == reservaId)
                .ToListAsync();
    }
}