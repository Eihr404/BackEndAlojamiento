using Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class Alojamiento_habitacionBusinessMapper
    {
        // De DataModel a Response (Lectura)
        public static Alojamiento_habitacionResponse ToResponse(alojamiento_habitacionesDataModel model)
        {
            return new Alojamiento_habitacionResponse
            {
                Id = model.Id,
                AlojamientoId = model.AlojamientoId,
                HabitacionId = model.HabitacionId,
                // Nota: Asegúrate de que el DTO Alojamiento_habitacionResponse tenga esta propiedad
                // NombreHabitacion = model.NombreHabitacion, 
                PrecioNoche = model.PrecioNoche,
                CantidadTotal = model.CantidadTotal
            };
        }

        // De Request a DataModel (Creación)
        public static alojamiento_habitacionesDataModel ToDataModel(CrearAlojamiento_habitacionRequest request)
        {
            return new alojamiento_habitacionesDataModel
            {
                AlojamientoId = request.AlojamientoId,
                HabitacionId = request.HabitacionId,
                PrecioNoche = request.PrecioNoche,
                CantidadTotal = request.CantidadTotal
            };
        }

        // De Request a DataModel (Actualización)
        public static alojamiento_habitacionesDataModel ToDataModel(ActualizarAlojamiento_habitacionRequest request)
        {
            return new alojamiento_habitacionesDataModel
            {
                Id = request.Id,
                AlojamientoId = request.AlojamientoId,
                HabitacionId = request.HabitacionId,
                PrecioNoche = request.PrecioNoche,
                CantidadTotal = request.CantidadTotal
            };
        }
    }
}