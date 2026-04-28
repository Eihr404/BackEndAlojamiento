using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class alojamientosDataMapper
    {
        public static alojamientosDataModel ToModel(this alojamientos entity) => new()
        {
            Id = entity.id,
            Nombre = entity.nombre,
            Tipo = entity.tipo.ToString(),
            Ciudad = entity.ciudad,
            Direccion = entity.direccion,
            CalificacionAvg = entity.calificacion_avg,
            TieneWifi = entity.tiene_wifi,
            TienePiscina = entity.tiene_piscina,
            AdmiteMascotas = entity.admite_mascotas,
            CheckIn = entity.check_in,
            CheckOut = entity.check_out
        };

        public static alojamientos ToEntity(this alojamientosDataModel model) => new()
        {
            id = model.Id,
            nombre = model.Nombre,
            tipo = model.Tipo,
            ciudad = model.Ciudad,
            direccion = model.Direccion,
            tiene_wifi = model.TieneWifi,
            tiene_piscina = model.TienePiscina,
            admite_mascotas = model.AdmiteMascotas,
            check_in = model.CheckIn,
            check_out = model.CheckOut,
            admin_id = model.AdminId
        };
    }
}
