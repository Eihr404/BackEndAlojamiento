using Microservicios.Alojamiento.Business.DTOs.Clientes;
using Microservicios.Alojamiento.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IClientesService
    {
        Task<ClienteResponse> CrearClienteAsync(CrearClienteRequest request);
        Task<ClienteResponse?> ObtenerPorIdAsync(Guid id);
        Task<PagedResult<ClienteResponse>> ObtenerFiltradosAsync(ClienteFiltroRequest filtro);
        Task<ClienteResponse> ObtenerPorUsuarioIdAsync(Guid usuarioId);
        Task<ClienteResponse> ActualizarClienteAsync(ActualizarClienteRequest request);

    }
}
