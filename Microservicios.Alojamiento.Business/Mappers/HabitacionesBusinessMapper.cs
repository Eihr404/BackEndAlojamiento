using Microservicios.Alojamiento.Business.DTOs.Habitaciones;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class HabitacionesBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static HabitacionesResponse ToResponse(habitacionesDataModel model)
        {
            return new HabitacionesResponse
            {
                Id = model.Id,
                NombreTipo = model.NombreTipo,
                CapacidadPersonas = model.CapacidadPersonas,
                NumeroCamas = model.NumeroCamas
            };
        }

        // De Request a DataModel (Creación de tipo de habitación)
        public static habitacionesDataModel ToDataModel(CrearHabitacionesRequest request)
        {
            return new habitacionesDataModel
            {
                NombreTipo = request.NombreTipo,
                CapacidadPersonas = request.CapacidadPersonas,
                NumeroCamas = request.NumeroCamas
            };
        }

        // De Request a DataModel (Actualización de tipo de habitación)
        public static habitacionesDataModel ToDataModel(ActualizarHabitacionesRequest request)
        {
            return new habitacionesDataModel
            {
                Id = request.Id,
                NombreTipo = request.NombreTipo,
                CapacidadPersonas = request.CapacidadPersonas,
                NumeroCamas = request.NumeroCamas
            };
        }
    }
}