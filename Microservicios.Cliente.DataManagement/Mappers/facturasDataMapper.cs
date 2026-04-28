using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class facturasDataMapper
    {
        public static facturasDataModel ToModel(this facturas entity) => new()
        {
            Id = entity.id,
            ReservaId = entity.reserva_id,
            MetodoPagoId = entity.metodo_pago_id, // <--- Sincronizado
            NumFactura = entity.num_factura,
            FechaEmision = entity.fecha_emision,
            EstadoPago = entity.estado_pago.ToString()
        };

        public static facturas ToEntity(this facturasDataModel model) => new()
        {
            id = model.Id,
            reserva_id = model.ReservaId,
            metodo_pago_id = model.MetodoPagoId, // <--- Sincronizado
            num_factura = model.NumFactura,
            // CORRECCIÓN: Usar EstadoFactura en lugar de EstadoPago
            estado_pago = model.EstadoPago
        };
    }
}