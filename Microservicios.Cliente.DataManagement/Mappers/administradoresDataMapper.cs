using Microservicios.Alojamiento.DataAccess.Entities; // <-- Asegúrate de que esto esté primero
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class administradoresDataMapper
    {
        // Fíjate que el parámetro sea EXACTAMENTE la entidad de DataAccess
        public static administradoresDataModel ToModel(this Microservicios.Alojamiento.DataAccess.Entities.administradores entity) => new()
        {
            Id = entity.id,
            UsuarioId = entity.usuario_id,
            NombreComercial = entity.nombre_comercial,
            NitTax = entity.nit_tax,
            TelefonoSoporte = entity.telefono_soporte
        };

        public static Microservicios.Alojamiento.DataAccess.Entities.administradores ToEntity(this administradoresDataModel model) => new()
        {
            id = model.Id,
            usuario_id = model.UsuarioId,
            nombre_comercial = model.NombreComercial,
            nit_tax = model.NitTax,
            telefono_soporte = model.TelefonoSoporte
        };
    }
}