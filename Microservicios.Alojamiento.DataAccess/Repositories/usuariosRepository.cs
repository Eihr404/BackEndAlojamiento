using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class usuariosRepository : RepositoryBase<usuarios>, IusuarioRepository
    {
        public usuariosRepository(AlojamientoDbContext context) : base(context) { }

        public async Task<usuarios?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.usuario_roles)
                    .ThenInclude(ur => ur.rol)
                .FirstOrDefaultAsync(u => u.email == email);
        }
    }
}
