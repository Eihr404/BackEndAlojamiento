using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface Iusuario_rolesDataService
    {
        // Asignar un rol a un usuario
        Task<bool> AssignAsync(usuario_rolesDataModel model);

        // Quitar un rol a un usuario
        Task<bool> RevokeAsync(Guid usuarioId, Guid rolId);

        // Listar todos los roles que tiene un usuario actualmente
        Task<IEnumerable<usuario_rolesDataModel>> GetRolesByUsuarioIdAsync(Guid usuarioId);

        // Verificar si un usuario tiene un rol específico (para autorización rápida)
        Task<bool> UserHasRolAsync(Guid usuarioId, string nombreRol);
    }
}