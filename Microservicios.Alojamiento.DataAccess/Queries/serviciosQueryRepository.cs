using Microsoft.EntityFrameworkCore;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class serviciosQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public serviciosQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<servicios?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Servicios
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.id == id);
        }
        public async Task<IEnumerable<servicios>> GetAllMasterServicesAsync() =>
            await _context.Servicios.AsNoTracking().OrderBy(s => s.nombre).ToListAsync();
    }
}