using Microservicios.Alojamiento.Business.DTOs.Servicios;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class ServiciosBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static ServiciosResponse ToResponse(serviciosDataModel model)
        {
            return new ServiciosResponse
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };
        }

        // De Request a DataModel (Creación de nuevo servicio en catálogo)
        public static serviciosDataModel ToDataModel(CrearServiciosRequest request)
        {
            return new serviciosDataModel
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
        }

        // De Request a DataModel (Actualización de servicio existente)
        public static serviciosDataModel ToDataModel(ActualizarServiciosRequest request)
        {
            return new serviciosDataModel
            {
                Id = request.Id,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
        }
    }
}