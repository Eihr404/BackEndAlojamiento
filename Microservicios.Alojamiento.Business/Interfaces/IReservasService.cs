using Microservicios.Alojamiento.Business.DTOs.Reservas;
using Microservicios.Alojamiento.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IReservasService
    {
        Task<ReservaResponse> CrearReservaAsync(CrearReservaRequest request);
        Task CambiarEstadoReservaAsync(Guid id, string nuevoEstado);

        Task<IEnumerable<ReservaResponse>> ObtenerReservasPorClienteAsync(Guid clienteId);

        Task<ReservaResponse?> ObtenerPorIdAsync(Guid id);

        Task<PagedResult<ReservaResponse>> ObtenerHistorialAsync(Guid usuarioId, bool esAdmin, int pagina, int registrosPorPagina);
        Task CancelarReservaAsync(Guid id);

        Task<IEnumerable<ReservaResponse>> ObtenerReservasPorAdminAsync(Guid adminId);
    }
}
