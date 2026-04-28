using Microservicios.Alojamiento.Business.DTOs.Metodos_pago;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class Metodos_pagoBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static Metodo_pagoResponse ToResponse(metodos_pagoDataModel model)
        {
            return new Metodo_pagoResponse
            {
                Id = model.Id,
                Nombre = model.Nombre
            };
        }

        // De Request a DataModel (Creación)
        public static metodos_pagoDataModel ToDataModel(CrearMetodo_pagoRequest request)
        {
            return new metodos_pagoDataModel
            {
                Nombre = request.Nombre
            };
        }

        // De Request a DataModel (Actualización)
        public static metodos_pagoDataModel ToDataModel(ActualizarMetodos_pagoRequest request)
        {
            return new metodos_pagoDataModel
            {
                Id = request.Id,
                Nombre = request.Nombre
            };
        }
    }
}