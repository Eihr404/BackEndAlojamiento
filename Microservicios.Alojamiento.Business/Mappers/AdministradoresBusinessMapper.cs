using Microservicios.Alojamiento.Business.DTOs.Administradores;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class AdministradoresBusinessMapper
    {
        // De DataModel a Response (Lectura)
        public static AdministradoresResponse ToResponse(administradoresDataModel model)
        {
            return new AdministradoresResponse
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                NombreComercial = model.NombreComercial,
                NitTax = model.NitTax,
                TelefonoSoporte = model.TelefonoSoporte
            };
        }

        // De Request a DataModel (Escritura/Creación)
        public static administradoresDataModel ToDataModel(CrearAdministradoresRequest request)
        {
            return new administradoresDataModel
            {
                UsuarioId = request.UsuarioId,
                NombreComercial = request.NombreComercial,
                NitTax = request.NitTax,
                TelefonoSoporte = request.TelefonoSoporte
            };
        }

        // De Request de Actualización a DataModel
        public static administradoresDataModel ToDataModel(ActualizarAdministradoresRequest request)
        {
            return new administradoresDataModel
            {
                Id = request.Id,
                UsuarioId = request.UsuarioId,
                NombreComercial = request.NombreComercial,
                NitTax = request.NitTax,
                TelefonoSoporte = request.TelefonoSoporte
            };
        }
    }
}