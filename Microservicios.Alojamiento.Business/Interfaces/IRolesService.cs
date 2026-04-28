using Microservicios.Alojamiento.Business.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<RolesResponse>> ObtenerRolesAsync();
        Task<RolesResponse> CrearRolAsync(CrearRolesRequest request);

        Task<RolesResponse> ActualizarRolAsync(ActualizarRolesRequest request);

        Task<RolesResponse> ObtenerPorIdAsync(Guid id);

        Task EliminarRolAsync(Guid id);


    }
}
