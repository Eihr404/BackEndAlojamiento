using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class metodos_pagoDataMapper
    {
        public static metodos_pagoDataModel ToModel(this metodos_pago entity) => new()
        {
            Id = entity.id,
            Nombre = entity.nombre
        };

        public static metodos_pago ToEntity(this metodos_pagoDataModel model) => new()
        {
            id = model.Id,
            nombre = model.Nombre
        };
    }
}