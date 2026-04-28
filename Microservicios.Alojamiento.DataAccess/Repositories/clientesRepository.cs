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
    public class clientesRepository : RepositoryBase<clientes>, IclientesRepository
    {
        public clientesRepository(AlojamientoDbContext context) : base(context) { }

        public async Task<clientes?> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.usuario_id == usuarioId);
        }
    }
}
