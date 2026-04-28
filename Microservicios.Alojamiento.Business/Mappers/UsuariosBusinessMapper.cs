using Microservicios.Alojamiento.Business.DTOs.Usuarios;
using Microservicios.Alojamiento.DataManagement.Models;
using System.Linq;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class UsuariosBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static UsuariosResponse ToResponse(usuariosDataModel model)
        {
            return new UsuariosResponse
            {
                Id = model.Id,
                Email = model.Email,
                Activo = model.Activo,
                FechaCreacion = model.FechaCreacion,
                // Si actualizaste el DTO UsuariosResponse para incluir Roles:
                // Roles = model.Roles 
            };
        }

        // De Request a DataModel (Creación de cuenta)
        public static usuariosDataModel ToDataModel(CrearUsuariosRequest request)
        {
            return new usuariosDataModel
            {
                Email = request.Email,
                Activo = request.Activo,
                FechaCreacion = System.DateTime.UtcNow,
                Roles = new System.Collections.Generic.List<string>()
                // Nota: El Password del Request se procesará en el Service (Hash) 
                // antes de enviarlo a la base de datos, por eso no está en este DataModel.
            };
        }

        // De Request a DataModel (Actualización de cuenta)
        public static usuariosDataModel ToDataModel(ActualizarUsuariosRequest request)
        {
            return new usuariosDataModel
            {
                Id = request.Id,
                Email = request.Email,
                Activo = request.Activo
                // La fecha de creación no se debe modificar en una actualización
            };
        }
    }
}