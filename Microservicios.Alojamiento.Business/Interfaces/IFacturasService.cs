using Microservicios.Alojamiento.Business.DTOs.Facturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IFacturasService
    {
        Task<FacturaResponse> GenerarFacturaAsync(CrearFacturaRequest request);
        Task<FacturaResponse?> ObtenerFacturaPorReservaAsync(Guid reservaId);
        Task ActualizarEstadoPagoAsync(Guid id, string nuevoEstado);
        Task<FacturaResponse> ObtenerPorNumeroAsync(string numFactura);
        Task<FacturaResponse> ObtenerPorIdAsync(Guid id);
    }
}
