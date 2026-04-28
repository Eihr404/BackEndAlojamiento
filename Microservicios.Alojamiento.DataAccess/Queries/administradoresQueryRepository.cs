using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class administradoresQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public administradoresQueryRepository(AlojamientoDbContext context) => _context = context;
        public async Task<bool> IsValidAdminAsync(Guid adminId) =>
            await _context.Administradores.AsNoTracking().AnyAsync(a => a.id == adminId);

        // ✅ AÑADIR: Buscar por ID
        public async Task<administradores?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Administradores
                .AsNoTracking()
                .Include(a => a.usuario) // Para traer datos del usuario base
                .FirstOrDefaultAsync(a => a.id == id);
        }

        // ✅ AÑADIR: Buscar por el ID de Usuario (Identity)
        public async Task<administradores?> ObtenerPorUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Administradores
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.usuario_id == usuarioId);
        }

    }
}
