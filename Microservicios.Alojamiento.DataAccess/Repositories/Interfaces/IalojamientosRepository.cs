using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories.Interfaces
{
    public interface IAlojamientosRepository : IRepositoryBase<alojamientos>
    {
        // Métodos específicos que no están en el CRUD genérico
        Task<IEnumerable<alojamientos>> GetByCiudadAsync(string ciudad);
        Task<IEnumerable<alojamientos>> GetByAdminAsync(Guid adminId);

        Task<PagedResult<alojamientos>> GetSearchPagedAsync(
            string? ciudad,
            decimal? precioMaximo,
            string? tipoAlojamiento,
            int pagina,
            int registrosPorPagina);
    }
}
