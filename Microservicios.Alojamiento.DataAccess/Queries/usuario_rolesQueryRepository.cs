using Microsoft.EntityFrameworkCore;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Queries
{
    public class usuario_rolesQueryRepository
    {
        private readonly AlojamientoDbContext _context;
        public usuario_rolesQueryRepository(AlojamientoDbContext context) => _context = context;
        // Añade este método debajo de GetRolesByUsuarioAsync
        public async Task<usuario_roles?> ObtenerAsignacionAsync(Guid usuarioId, Guid rolId)
        {
            return await _context.UsuarioRoles
                .AsNoTracking()
                .FirstOrDefaultAsync(ur => ur.usuario_id == usuarioId && ur.rol_id == rolId);
        }
        // Cambiamos <string> por <usuario_roles> e incluimos la tabla rol
        public async Task<IEnumerable<usuario_roles>> GetRolesByUsuarioAsync(Guid usuarioId) =>
            await _context.UsuarioRoles.AsNoTracking()
                .Include(ur => ur.rol) // Incluimos el rol para poder ver su nombre
                .Where(ur => ur.usuario_id == usuarioId)
                .ToListAsync();   
    }
}