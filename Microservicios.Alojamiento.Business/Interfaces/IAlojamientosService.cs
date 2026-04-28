using Microservicios.Alojamiento.Business.DTOs.Alojamientos;
using Microservicios.Alojamiento.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IAlojamientosService
    {
        Task<AlojamientoResponse> CrearAlojamientoAsync(CrearAlojamientoRequest request);
        Task<AlojamientoResponse> ActualizarAlojamientoAsync(ActualizarAlojamientoRequest request);
        Task EliminarAlojamientoAsync(Guid id);
        Task<AlojamientoResponse> ObtenerPorIdAsync(Guid id);
        Task<PagedResult<AlojamientoResponse>> ObtenerFiltradosAsync(AlojamientoFiltroRequest filtro);
    }
}
