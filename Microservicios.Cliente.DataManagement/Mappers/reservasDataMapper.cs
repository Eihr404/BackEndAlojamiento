using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class reservasDataMapper
    {
        // 1. Lectura: Entity -> Model
        public static reservasDataModel ToModel(this reservas entity) => new()
        {
            Id = entity.id,
            ClienteId = entity.cliente_id,
            AlojamientoId = entity.alojamiento_id,
            FechaSolicitud = entity.fecha_solicitud,
            FechaCheckIn = entity.fecha_check_in,
            FechaCheckOut = entity.fecha_check_out,
            MontoTotal = entity.monto_total,
            Estado = entity.estado.ToString(),
            // Mapeamos los detalles si vienen incluidos en la consulta
            Detalles = entity.detalles?.Select(d => d.ToModel()).ToList() ?? new()
        };

        // 2. Escritura: Model -> Entity (SOLO UNO)
        public static reservas ToEntity(this reservasDataModel model) => new()
        {
            id = model.Id,
            cliente_id = model.ClienteId,
            alojamiento_id = model.AlojamientoId,
            fecha_check_in = model.FechaCheckIn,
            fecha_check_out = model.FechaCheckOut,
            monto_total = model.MontoTotal,
            estado = model.Estado,
            fecha_solicitud = model.FechaSolicitud == default ? DateTime.UtcNow : model.FechaSolicitud
        };
    }
}