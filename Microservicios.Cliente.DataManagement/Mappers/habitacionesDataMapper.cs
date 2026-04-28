using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class habitacionesDataMapper
    {
        public static habitacionesDataModel ToModel(this habitaciones entity) => new()
        {
            Id = entity.id,
            NombreTipo = entity.nombre_tipo,
            CapacidadPersonas = entity.capacidad_personas,
            NumeroCamas = entity.numero_camas
        };

        public static habitaciones ToEntity(this habitacionesDataModel model) => new()
        {
            id = model.Id,
            nombre_tipo = model.NombreTipo,
            capacidad_personas = model.CapacidadPersonas,
            numero_camas = model.NumeroCamas
        };
    }
}
