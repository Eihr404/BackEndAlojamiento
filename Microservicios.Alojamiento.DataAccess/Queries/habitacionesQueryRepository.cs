using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class habitacionesQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public habitacionesQueryRepository(AlojamientoDbContext context) => _context = context;

        // El que ya tenías
        public async Task<IEnumerable<habitaciones>> GetTiposDisponiblesAsync() =>
            await _context.Habitaciones.AsNoTracking().ToListAsync();

        // ✅ AGREGAR: Buscar por ID (Resuelve el error CS1061)
        public async Task<habitaciones?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Habitaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.id == id);
        }

        // ✅ RECOMENDADO: Buscar habitaciones de un alojamiento específico
        public async Task<IEnumerable<habitaciones>> GetByAlojamientoIdAsync(Guid alojamientoId)
        {
            // Usamos el 'Join' o la navegación desde la tabla intermedia
            return await _context.AlojamientoHabitaciones
                .AsNoTracking()
                .Where(ah => ah.alojamiento_id == alojamientoId)
                .Select(ah => ah.habitacion) // Navegamos directamente a la habitación
                .ToListAsync();
        }

        // Añade este método si no lo tienes para que sea genérico
        public async Task<IEnumerable<habitaciones>> GetAllAsync()
        {
            return await _context.Habitaciones
                .AsNoTracking()
                .ToListAsync();
        }
    }
}