using Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IServicios_alojamientoService
    {
        Task<Servicios_alojamientoResponse> AsignarServicioAAlojamientoAsync(CrearServicios_alojamientoRequest request);
        Task<Servicios_alojamientoResponse> ActualizarServicioAsignadoAsync(ActualizarServicios_alojamientoRequest request);
        Task<IEnumerable<Servicios_alojamientoResponse>> ObtenerServiciosPorAlojamientoAsync(Guid alojamientoId);

        Task QuitarServicioDeAlojamientoAsync(Guid id);
        Task<Servicios_alojamientoResponse> ObtenerPorIdAsync(Guid id);
    }
}
