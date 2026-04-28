using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories.Interfaces
{
    public interface IfacturasRepository : IRepositoryBase<facturas>
    {
        Task<facturas?> GetByReservaIdAsync(Guid reservaId);
    }
}
