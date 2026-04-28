using Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class Servicios_alojamientoBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static Servicios_alojamientoResponse ToResponse(servicios_alojamientoDataModel model)
        {
            return new Servicios_alojamientoResponse
            {
                Id = model.Id,
                AlojamientoId = model.AlojamientoId,
                ServicioId = model.ServicioId,
                PrecioAdicional = model.PrecioAdicional,
                EstaActivo = model.EstaActivo
                // Nota: Si decides mostrar el nombre descriptivo del servicio en el DTO, 
                // puedes habilitar la siguiente línea:
                // NombreServicio = model.NombreServicio
            };
        }

        // De Request a DataModel (Creación de vínculo servicio-alojamiento)
        public static servicios_alojamientoDataModel ToDataModel(CrearServicios_alojamientoRequest request)
        {
            return new servicios_alojamientoDataModel
            {
                AlojamientoId = request.AlojamientoId,
                ServicioId = request.ServicioId,
                PrecioAdicional = request.PrecioAdicional,
                EstaActivo = request.EstaActivo
            };
        }

        // De Request a DataModel (Actualización de precio o estado)
        public static servicios_alojamientoDataModel ToDataModel(ActualizarServicios_alojamientoRequest request)
        {
            return new servicios_alojamientoDataModel
            {
                Id = request.Id,
                AlojamientoId = request.AlojamientoId,
                ServicioId = request.ServicioId,
                PrecioAdicional = request.PrecioAdicional,
                EstaActivo = request.EstaActivo
            };
        }
    }
}