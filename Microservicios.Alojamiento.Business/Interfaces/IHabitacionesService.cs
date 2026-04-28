using Microservicios.Alojamiento.Business.DTOs.Habitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IHabitacionesService
    {
        Task<HabitacionesResponse> CrearHabitacionAsync(CrearHabitacionesRequest request);
        Task<HabitacionesResponse> ActualizarHabitacionAsync(ActualizarHabitacionesRequest request);
        Task EliminarHabitacionAsync(Guid id);
        Task<HabitacionesResponse?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<HabitacionesResponse>> ObtenerTodasAsync();

        Task<IEnumerable<HabitacionesResponse>> ObtenerPorAlojamientoAsync(Guid alojamientoId);

    }
}
