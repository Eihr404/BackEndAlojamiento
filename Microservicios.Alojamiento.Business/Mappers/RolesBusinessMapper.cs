using Microservicios.Alojamiento.Business.DTOs.Roles;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class RolesBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static RolesResponse ToResponse(rolesDataModel model)
        {
            return new RolesResponse
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };
        }

        // De Request a DataModel (Creación de nuevo rol)
        public static rolesDataModel ToDataModel(CrearRolesRequest request)
        {
            return new rolesDataModel
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
        }

        // De Request a DataModel (Actualización de rol existente)
        public static rolesDataModel ToDataModel(ActualizarRolesRequest request)
        {
            return new rolesDataModel
            {
                Id = request.Id,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
        }
    }
}