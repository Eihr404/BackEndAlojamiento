using Microservicios.Alojamiento.Business.DTOs.Facturas;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class FacturasBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static FacturaResponse ToResponse(facturasDataModel model)
        {
            return new FacturaResponse
            {
                Id = model.Id,
                ReservaId = model.ReservaId,
                NumFactura = model.NumFactura,
                FechaEmision = model.FechaEmision,
                MetodoPagoId = model.MetodoPagoId,
                EstadoPago = model.EstadoPago
            };
        }

        // De Request a DataModel (Creación de factura)
        public static facturasDataModel ToDataModel(CrearFacturaRequest request)
        {
            return new facturasDataModel
            {
                ReservaId = request.ReservaId,
                NumFactura = request.NumFactura,
                MetodoPagoId = request.MetodoPagoId,
                // Nota: La fecha de emisión y el estado inicial suelen 
                // establecerse en el servicio o por defecto en la DB
                FechaEmision = DateTime.UtcNow,
                EstadoPago = "Exitoso"
            };
        }

        // De Request a DataModel (Actualización de estado de pago)
        public static facturasDataModel ToDataModel(ActualizarFacturaRequest request)
        {
            return new facturasDataModel
            {
                Id = request.Id,
                EstadoPago = request.EstadoPago,
                MetodoPagoId = request.MetodoPagoId
            };
        }
    }
}