using Microservicios.Alojamiento.Business.DTOs.Auditoria;
using Microservicios.Alojamiento.Business.DTOs.Auditoria.Microservicios.Alojamiento.Business.DTOs.Auditoria;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class AuditoriaBusinessMapper
    {
        // De DataModel a Response (Lectura)
        public static AuditoriaResponse ToResponse(auditoriaDataModel model)
        {
            return new AuditoriaResponse
            {
                Id = model.Id,
                Accion = model.Accion,
                TablaAfectada = model.TablaAfectada,
                RegistroId = model.RegistroId,
                UsuarioId = model.UsuarioId,
                FechaHora = model.FechaHora,
                DatosAnteriores = model.DatosAnteriores
            };
        }

        // De Request a DataModel (Creación)
        public static auditoriaDataModel ToDataModel(CrearAuditoriaRequest request)
        {
            return new auditoriaDataModel
            {
                Accion = request.Accion,
                TablaAfectada = request.TablaAfectada,
                // Si el DTO permite nulos en RegistroId, lo manejamos según tu DataModel (Guid)
                RegistroId = request.RegistroId ?? Guid.Empty,
                UsuarioId = request.UsuarioId,
                DatosAnteriores = request.DatosAnteriores
            };
        }

        // De Request a DataModel (Actualización)
        public static auditoriaDataModel ToDataModel(ActualizarAuditoriaRequest request)
        {
            return new auditoriaDataModel
            {
                Id = request.Id,
                Accion = request.Accion,
                TablaAfectada = request.TablaAfectada,
                RegistroId = request.RegistroId ?? Guid.Empty,
                UsuarioId = request.UsuarioId,
                DatosAnteriores = request.DatosAnteriores
            };
        }
    }
}