using Microservicios.Alojamiento.Business.DTOs.Reserva_detalles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IReserva_detallesService
    {
        Task<Reserva_detalleResponse> AgregarDetalleReservaAsync(CrearReserva_detallesRequest request);
        Task<Reserva_detalleResponse> ActualizarDetalleReservaAsync(ActualizarReserva_detallesRequest request);
        Task EliminarDetalleReservaAsync(Guid id);
        Task<IEnumerable<Reserva_detalleResponse>> ObtenerDetallesPorReservaAsync(Guid reservaId);
    }
}
