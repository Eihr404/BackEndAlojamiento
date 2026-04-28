using Microservicios.Alojamiento.Business.DTOs.Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IServiciosService
    {
        Task<ServiciosResponse> CrearServicioAsync(CrearServiciosRequest request);
        Task<ServiciosResponse> ActualizarServicioAsync(ActualizarServiciosRequest request);
        Task EliminarServicioAsync(Guid id);
        Task<ServiciosResponse?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<ServiciosResponse>> ObtenerTodosAsync();
    }
}
