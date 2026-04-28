using Microservicios.Alojamiento.Business.DTOs.Alojamientos;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class AlojamientosBusinessMapper
    {
        // De DataModel a Response (Lectura)
        public static AlojamientoResponse ToResponse(alojamientosDataModel model)
        {
            return new AlojamientoResponse
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Tipo = model.Tipo,
                Ciudad = model.Ciudad,
                Direccion = model.Direccion,
                CalificacionAvg = model.CalificacionAvg,
                TieneWifi = model.TieneWifi,
                TienePiscina = model.TienePiscina,
                AdmiteMascotas = model.AdmiteMascotas,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                AdminId = model.AdminId
            };
        }

        // De Request a DataModel (Escritura/Creación)
        public static alojamientosDataModel ToDataModel(CrearAlojamientoRequest request)
        {
            return new alojamientosDataModel
            {
                Nombre = request.Nombre,
                Tipo = request.Tipo,
                Ciudad = request.Ciudad,
                Direccion = request.Direccion,
                TieneWifi = request.TieneWifi,
                TienePiscina = request.TienePiscina,
                AdmiteMascotas = request.AdmiteMascotas,
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                AdminId = request.AdminId
            };
        }
    }
}