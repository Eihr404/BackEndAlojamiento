using Microservicios.Alojamiento.Business.DTOs.Reserva_detalles;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class Reserva_detallesBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static Reserva_detalleResponse ToResponse(reserva_detallesDataModel model)
        {
            return new Reserva_detalleResponse
            {
                Id = model.Id,
                ReservaId = model.ReservaId,
                TipoItem = model.TipoItem,
                ItemId = model.ItemId,
                Cantidad = model.Cantidad,
                PrecioCapturado = model.PrecioCapturado
            };
        }

        // De Request a DataModel (Creación de detalle)
        public static reserva_detallesDataModel ToDataModel(CrearReserva_detallesRequest request)
        {
            return new reserva_detallesDataModel
            {
                ReservaId = request.ReservaId,
                TipoItem = request.TipoItem,
                ItemId = request.ItemId,
                Cantidad = request.Cantidad,
                PrecioCapturado = request.PrecioCapturado
            };
        }

        // De Request a DataModel (Actualización de detalle)
        public static reserva_detallesDataModel ToDataModel(ActualizarReserva_detallesRequest request)
        {
            return new reserva_detallesDataModel
            {
                Id = request.Id,
                ReservaId = request.ReservaId,
                TipoItem = request.TipoItem,
                ItemId = request.ItemId,
                Cantidad = request.Cantidad,
                PrecioCapturado = request.PrecioCapturado
            };
        }
    }
}