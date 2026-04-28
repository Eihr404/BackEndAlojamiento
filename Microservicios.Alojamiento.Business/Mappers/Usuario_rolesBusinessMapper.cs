using Microservicios.Alojamiento.Business.DTOs.Usuario_roles;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class Usuario_rolesBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static Usuario_rolesResponse ToResponse(usuario_rolesDataModel model)
        {
            return new Usuario_rolesResponse
            {
                UsuarioId = model.UsuarioId,
                RolId = model.RolId
                // Si agregaste NombreRol al DTO Usuario_rolesResponse, mapealo aquí:
                // NombreRol = model.NombreRol
            };
        }

        // De Request a DataModel (Creación/Asignación de rol)
        public static usuario_rolesDataModel ToDataModel(CrearUsuario_rolesRequest request)
        {
            return new usuario_rolesDataModel
            {
                UsuarioId = request.UsuarioId,
                RolId = request.RolId
            };
        }

        // De Request a DataModel (Para actualización o baja/alta lógica)
        public static usuario_rolesDataModel ToDataModel(ActualizarUsuario_rolesRequest request)
        {
            return new usuario_rolesDataModel
            {
                UsuarioId = request.UsuarioId,
                RolId = request.RolIdNuevo // Mapeamos el nuevo rol solicitado
            };
        }
    }
}