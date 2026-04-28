using Microsoft.EntityFrameworkCore;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Common;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class alojamientosQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public alojamientosQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<PagedResult<alojamientos>> GetPagedSearchAsync(
        string? ciudad,       
        decimal? maxPrecio,
        int page,
        int size,
        string? tipo,
        Guid? adminId = null,
        bool? tieneWifi = null,
        bool? tienePiscina = null,
        bool? admiteMascotas = null,
        bool? tieneCocina = null)
        {
            var query = _context.Alojamientos.AsNoTracking()
                .Include(a => a.habitaciones_configuradas)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ciudad))
                query = query.Where(a => a.ciudad.Contains(ciudad));

            if (maxPrecio.HasValue)
                query = query.Where(a => a.habitaciones_configuradas.Any(h => h.precio_noche <= maxPrecio));

            if (adminId.HasValue)
                query = query.Where(a => a.admin_id == adminId.Value);

            if (!string.IsNullOrEmpty(tipo))        // ← agregar
                query = query.Where(a => a.tipo == tipo);

            if (tieneWifi == true)
                query = query.Where(a => a.tiene_wifi == true);

            if (tienePiscina == true)
                query = query.Where(a => a.tiene_piscina == true);

            if (admiteMascotas == true)
                query = query.Where(a => a.admite_mascotas == true);

            if (tieneCocina == true)
                query = query.Where(a => a.tiene_cocina == true);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PagedResult<alojamientos>(items, total, page, size);
        }

        public async Task<alojamientos?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Alojamientos
                .AsNoTracking() // Importante para Queries: mejora el rendimiento
                .FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<bool> ExistePorAdminIdAsync(Guid adminId)
        {
            return await _context.Alojamientos
                .AsNoTracking()
                .AnyAsync(a => a.admin_id == adminId);
        }
    }
}