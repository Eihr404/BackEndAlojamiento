using Microservicios.Alojamiento.Business.DTOs.Reservas;
using Microservicios.Alojamiento.DataManagement.Models;
using System.Linq;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class ReservasBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static ReservaResponse ToResponse(reservasDataModel model)
        {
            return new ReservaResponse
            {
                Id = model.Id,
                ClienteId = model.ClienteId,
                AlojamientoId = model.AlojamientoId,
                FechaSolicitud = model.FechaSolicitud,
                FechaCheckIn = model.FechaCheckIn,
                FechaCheckOut = model.FechaCheckOut,
                Estado = model.Estado,
                MontoTotal = model.MontoTotal
                // Si el DTO ReservaResponse tuviera una lista de detalles, se mapearía aquí:
                // Detalles = model.Detalles.Select(d => Reserva_detallesBusinessMapper.ToResponse(d)).ToList()
            };
        }

        // De Request a DataModel (Creación de nueva reserva)
        public static reservasDataModel ToDataModel(CrearReservaRequest request)
        {
            return new reservasDataModel
            {
                ClienteId = request.ClienteId,
                AlojamientoId = request.AlojamientoId,
                FechaCheckIn = request.FechaCheckIn,
                FechaCheckOut = request.FechaCheckOut,
                MontoTotal = request.MontoTotal,
                // Valores iniciales por defecto para el negocio
                FechaSolicitud = System.DateTime.UtcNow,
                Estado = "Pendiente", // Valor inicial según el ENUM estado_reserva
                Detalles = new System.Collections.Generic.List<reserva_detallesDataModel>()
            };
        }

        // De Request a DataModel (Actualización de reserva existente)
        public static reservasDataModel ToDataModel(ActualizarReservaRequest request)
        {
            return new reservasDataModel
            {
                Id = request.Id,
                ClienteId = request.ClienteId,
                AlojamientoId = request.AlojamientoId,
                FechaCheckIn = request.FechaCheckIn,
                FechaCheckOut = request.FechaCheckOut,
                MontoTotal = request.MontoTotal,
                Estado = request.Estado
            };
        }
    }
}