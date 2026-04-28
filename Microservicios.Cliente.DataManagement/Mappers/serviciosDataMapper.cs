using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class serviciosDataMapper
    {
        public static serviciosDataModel ToModel(this servicios entity) => new()
        {
            Id = entity.id,
            Nombre = entity.nombre,
            Descripcion = entity.descripcion
        };

        public static servicios ToEntity(this serviciosDataModel model) => new()
        {
            id = model.Id,
            nombre = model.Nombre,
            descripcion = model.Descripcion
        };
    }
}
