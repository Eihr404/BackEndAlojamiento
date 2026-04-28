using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class alojamiento_habitacionDataMapper
    {
        public static alojamiento_habitacionesDataModel ToModel(this alojamiento_habitacion entity) => new()
        {
            Id = entity.id, // Sincronizado
            AlojamientoId = entity.alojamiento_id,
            HabitacionId = entity.habitacion_id,
            PrecioNoche = entity.precio_noche,
            CantidadTotal = entity.cantidad_total, // Sincronizado con Entity
            NombreHabitacion = entity.habitacion?.nombre_tipo ?? string.Empty
        };

        public static alojamiento_habitacion ToEntity(this alojamiento_habitacionesDataModel model) => new()
        {
            id = model.Id, // Sincronizado
            alojamiento_id = model.AlojamientoId,
            habitacion_id = model.HabitacionId,
            precio_noche = model.PrecioNoche,
            cantidad_total = model.CantidadTotal // Sincronizado con Entity
        };
    }
}