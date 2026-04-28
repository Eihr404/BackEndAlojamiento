using System;
using System.Collections.Generic;
using System.Text;

using Microservicios.Alojamiento.DataAccess.Entities;

namespace Microservicios.Alojamiento.DataAccess.Repositories.Interfaces
{
    public interface IreservasRepository : IRepositoryBase<reservas>
    {
        Task<IEnumerable<reservas>> GetByClienteAsync(Guid clienteId);
        Task<IEnumerable<reservas>> GetByEstadoAsync(string estado);
    }
}