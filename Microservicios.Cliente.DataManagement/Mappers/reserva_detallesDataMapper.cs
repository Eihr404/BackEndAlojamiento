using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class reserva_detallesDataMapper
    {
        // Entity -> Model
        public static reserva_detallesDataModel ToModel(this reserva_detalles entity) => new()
        {
            Id = entity.id,
            ReservaId = entity.reserva_id,
            TipoItem = entity.tipo_item.ToString(),
            ItemId = entity.item_id,
            Cantidad = entity.cantidad,
            PrecioCapturado = entity.precio_capturado
        };

        // Model -> Entity
        public static reserva_detalles ToEntity(this reserva_detallesDataModel model) => new()
        {
            id = model.Id,
            reserva_id = model.ReservaId,
            // CORRECCIÓN: Usar el nombre exacto del Enum definido en la Entidad
            tipo_item = model.TipoItem,
            item_id = model.ItemId,
            cantidad = model.Cantidad,
            precio_capturado = model.PrecioCapturado
        };
    }
}