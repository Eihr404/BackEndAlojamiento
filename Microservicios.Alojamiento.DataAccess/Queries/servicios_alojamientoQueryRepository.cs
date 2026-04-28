using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class servicios_alojamientoQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public servicios_alojamientoQueryRepository(AlojamientoDbContext context) => _context = context;
        public async Task<servicios_alojamiento?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.ServiciosAlojamientos
                .AsNoTracking()
                .Include(sa => sa.servicio) // Traemos el catálogo base
                .Include(sa => sa.alojamiento) // Opcional, por si necesitas datos del hotel
                .FirstOrDefaultAsync(sa => sa.id == id);
        }
        public async Task<IEnumerable<servicios_alojamiento>> GetServiciosActivosAsync(Guid alojamientoId) =>
            await _context.ServiciosAlojamientos.AsNoTracking()
                .Include(sa => sa.servicio)
                .Where(sa => sa.alojamiento_id == alojamientoId && sa.esta_activo)
                .ToListAsync();
    }
}
