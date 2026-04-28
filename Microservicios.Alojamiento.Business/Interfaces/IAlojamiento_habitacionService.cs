using Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IAlojamiento_habitacionService
    {
        Task<Alojamiento_habitacionResponse> AsignarHabitacionAAlojamientoAsync(CrearAlojamiento_habitacionRequest request);
        Task<Alojamiento_habitacionResponse> ActualizarPrecioOCantidadAsync(ActualizarAlojamiento_habitacionRequest request);
        Task EliminarRelacionAsync(Guid id);
        Task<IEnumerable<Alojamiento_habitacionResponse>> ObtenerHabitacionesPorAlojamientoAsync(Guid alojamientoId);

        Task<IEnumerable<Alojamiento_habitacionResponse>> ObtenerPreciosPorAlojamientoAsync(Guid alojamientoId);
    }
}
