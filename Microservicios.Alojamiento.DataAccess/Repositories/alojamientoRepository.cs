using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; // Necesario para .Where, .Skip, .Take
using System.Threading.Tasks; // Necesario para Task<>

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class alojamientoRepository : RepositoryBase<alojamientos>, IAlojamientosRepository
    {
        public alojamientoRepository(AlojamientoDbContext context) : base(context) { }

        // --- ESTE ES EL MÉTODO QUE ESTABA EXIGIENDO LA INTERFAZ ---
        public async Task<PagedResult<alojamientos>> GetSearchPagedAsync(
     string? ciudad,
     decimal? precioMaximo,
     string? tipoAlojamiento,
     int pagina,
     int registrosPorPagina)
        {
            var query = _dbSet.AsQueryable();

            // 1. Filtro Ciudad
            if (!string.IsNullOrEmpty(ciudad))
                query = query.Where(a => a.ciudad.Contains(ciudad));

            // 2. Filtro Tipo Alojamiento (ARREGLO DEL CS0019)
            // Intentamos convertir el string al Enum. Ignoramos mayúsculas/minúsculas con "true"
            if (!string.IsNullOrEmpty(tipoAlojamiento))
            {
                // Como 'a.tipo' ahora es string, simplemente comparamos texto con texto.
                // Usamos .ToLower() para que la búsqueda sea insensible a mayúsculas/minúsculas
                query = query.Where(a => a.tipo.ToLower() == tipoAlojamiento.ToLower());
            }

            // 3. Filtro Precio Maximo (ARREGLO DEL CS1061)
            if (precioMaximo.HasValue)
            {
                // ⚠️ IMPORTANTE: Si te sigue marcando error aquí, ve a tu clase 'alojamientos.cs'
                // y busca cómo llamaste a la colección de habitaciones. Podría ser:
                // a.Habitaciones.Any(...) o a.AlojamientoHabitaciones.Any(...)
                query = query.Where(a => a.habitaciones_configuradas.Any(h => h.precio_noche <= precioMaximo));
            }

            // Calculamos el total antes de paginar
            var total = await query.CountAsync();

            // Paginamos
            var items = await query
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            return new PagedResult<alojamientos>(items, total, pagina, registrosPorPagina);
        }

        // --- TUS MÉTODOS ORIGINALES INTACTOS ---
        public async Task<IEnumerable<alojamientos>> GetByCiudadAsync(string ciudad) =>
            await _context.Alojamientos.Where(a => a.ciudad.Contains(ciudad)).ToListAsync();

        public async Task<IEnumerable<alojamientos>> GetByAdminAsync(Guid adminId) =>
            await _context.Alojamientos.Where(a => a.admin_id == adminId).ToListAsync();
    }
}
