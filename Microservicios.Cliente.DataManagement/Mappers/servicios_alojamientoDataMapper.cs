using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class servicios_alojamientoDataMapper
    {
        public static servicios_alojamientoDataModel ToModel(this servicios_alojamiento entity) => new()
        {
            Id = entity.id, // Mapeamos el ID real de la tabla
            AlojamientoId = entity.alojamiento_id,
            ServicioId = entity.servicio_id,
            NombreServicio = entity.servicio?.nombre ?? string.Empty,
            PrecioAdicional = entity.precio_adicional, // Sincronizado con Entity
            EstaActivo = entity.esta_activo
        };

        public static servicios_alojamiento ToEntity(this servicios_alojamientoDataModel model) => new()
        {
            id = model.Id,
            alojamiento_id = model.AlojamientoId,
            servicio_id = model.ServicioId,
            precio_adicional = model.PrecioAdicional, // Sincronizado con Entity
            esta_activo = model.EstaActivo
        };
    }
}