using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class alojamiento_habitacionQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public alojamiento_habitacionQueryRepository(AlojamientoDbContext context) => _context = context;
        public async Task<IEnumerable<alojamiento_habitacion>> GetPreciosByAlojamientoAsync(Guid alojamientoId) =>
            await _context.AlojamientoHabitaciones.AsNoTracking()
                .Include(ah => ah.habitacion)
                .Where(ah => ah.alojamiento_id == alojamientoId)
                .ToListAsync();
        public async Task<alojamiento_habitacion?> GetByAlojamientoYHabitacionAsync(Guid alojamientoId, Guid habitacionId)
        {
            return await _context.AlojamientoHabitaciones
                // sin AsNoTracking para que EF rastree los cambios
                .FirstOrDefaultAsync(ah =>
                    ah.alojamiento_id == alojamientoId &&
                    ah.habitacion_id == habitacionId);
        }

        // ✅ AÑADIR: Buscar configuración específica por ID
        public async Task<alojamiento_habitacion?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.AlojamientoHabitaciones
                .Include(ah => ah.alojamiento)
                .Include(ah => ah.habitacion)
                .FirstOrDefaultAsync(ah => ah.id == id);
        }
    }
}
