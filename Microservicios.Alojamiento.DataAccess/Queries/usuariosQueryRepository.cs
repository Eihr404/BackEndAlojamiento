using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class usuariosQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public usuariosQueryRepository(AlojamientoDbContext context) => _context = context;

        public async Task<object?> GetUserClaimsDataAsync(Guid userId)
        {
            return await _context.Usuarios.AsNoTracking()
                .Where(u => u.id == userId)
                .Select(u => new {
                    u.email,
                    Roles = u.usuario_roles.Select(r => r.rol.nombre).ToList(),
                    PerfilId = u.cliente != null ? u.cliente.id : (Guid?)null  // ← sin el !
                })
                .FirstOrDefaultAsync();
        }
        // Añade este método debajo de los que ya tienes
        public async Task<usuarios?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.usuario_roles)
                    .ThenInclude(ur => ur.rol)
                .FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<usuarios?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.usuario_roles)
                    .ThenInclude(ur => ur.rol)
                //.AsSplitQuery()                          // ← agrega esto
                .FirstOrDefaultAsync(u => u.email == email);
        }
    }
}
