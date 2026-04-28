using Microservicios.Alojamiento.Business.DTOs.Clientes;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class ClientesBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static ClienteResponse ToResponse(clientesDataModel model)
        {
            return new ClienteResponse
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Telefono = model.Telefono,
                DocumentoIdentidad = model.DocumentoIdentidad
            };
        }

        // De Request a DataModel (Creación de nuevo cliente)
        public static clientesDataModel ToDataModel(CrearClienteRequest request)
        {
            return new clientesDataModel
            {
                UsuarioId = request.UsuarioId,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                DocumentoIdentidad = request.DocumentoIdentidad ?? string.Empty
            };
        }

        // De Request a DataModel (Actualización de perfil existente)
        public static clientesDataModel ToDataModel(ActualizarClienteRequest request)
        {
            return new clientesDataModel
            {
                Id = request.Id,
                UsuarioId = request.UsuarioId,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                DocumentoIdentidad = request.DocumentoIdentidad ?? string.Empty
            };
        }
    }
}