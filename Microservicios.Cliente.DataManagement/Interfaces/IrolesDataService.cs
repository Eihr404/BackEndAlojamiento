using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IrolesDataService
    {
        Task<IEnumerable<rolesDataModel>> GetAllAsync();
        Task<rolesDataModel?> GetByNameAsync(string nombreRol);
        Task<bool> AssignRolToUsuarioAsync(Guid usuarioId, Guid rolId);
        Task<bool> RemoveRolFromUsuarioAsync(Guid usuarioId, Guid rolId);
        Task<IEnumerable<string>> GetRolesByUsuarioAsync(Guid usuarioId);
    }
}
