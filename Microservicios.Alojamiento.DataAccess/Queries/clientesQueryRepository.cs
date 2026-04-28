using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class clientesQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public async Task<clientes?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.usuario) // Traemos el usuario base por si acaso
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task<clientes?> ObtenerPorUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.usuario)
                .FirstOrDefaultAsync(c => c.usuario_id == usuarioId);
        }
        public clientesQueryRepository(AlojamientoDbContext context) => _context = context;
        public async Task<clientes?> GetFullProfileAsync(Guid clienteId) =>
            await _context.Clientes.AsNoTracking()
                .Include(c => c.usuario)
                .FirstOrDefaultAsync(c => c.id == clienteId);

        public async Task<PagedResult<clientes>> ObtenerFiltradosPagedAsync(
    string? nombre, string? apellido, string? documento, int pagina, int size)
        {
            var query = _context.Clientes
                .AsNoTracking()
                .Include(c => c.usuario)
                .AsQueryable();

            // Filtros dinámicos (solo se aplican si el dato no viene nulo o vacío)
            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(c => c.nombre.Contains(nombre));

            if (!string.IsNullOrWhiteSpace(apellido))
                query = query.Where(c => c.apellido.Contains(apellido));

            if (!string.IsNullOrWhiteSpace(documento))
                query = query.Where(c => c.documento_identidad != null && c.documento_identidad.Contains(documento));

            // Conteo total para la paginación
            var total = await query.CountAsync();

            // Obtener solo la página solicitada
            var items = await query.OrderBy(c => c.nombre).ThenBy(c => c.apellido)
                                   .Skip((pagina - 1) * size)
                                   .Take(size)
                                   .ToListAsync();

            return new PagedResult<clientes>(items, total, pagina, size);
        }
        }
}
