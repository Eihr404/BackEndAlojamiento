using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class metodos_pagoQueryRepository
    {

        private readonly AlojamientoDbContext _context;

        // Añade este método debajo de GetAllAsync
        public async Task<metodos_pago?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.MetodosPago
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);
        }
        public metodos_pagoQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<IEnumerable<metodos_pago>> GetAllAsync() =>
            await _context.MetodosPago.AsNoTracking().ToListAsync();
    }
}
