using Microservicios.Alojamiento.Business.DTOs.Usuario_roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IUsuario_rolesService
    {
        Task<Usuario_rolesResponse> AsignarRolAUsuarioAsync(CrearUsuario_rolesRequest request);
        Task EliminarRolDeUsuarioAsync(Guid usuarioId, Guid rolId);
        Task<IEnumerable<Usuario_rolesResponse>> ObtenerRolesPorUsuarioAsync(Guid usuarioId);
    }
}
