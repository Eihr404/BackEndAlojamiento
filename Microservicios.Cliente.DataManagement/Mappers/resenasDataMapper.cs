using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class resenasDataMapper
    {
        public static resenasDataModel ToModel(this resenas entity) => new()
        {
            Id = entity.id,
            ClienteId = entity.cliente_id,
            AlojamientoId = entity.alojamiento_id,
            Estrellas = entity.estrellas,
            Comentario = entity.comentario,
            Fecha = entity.fecha,
            NombreCliente = entity.cliente != null ? $"{entity.cliente.nombre} {entity.cliente.apellido}" : "Anónimo"
        };

        public static resenas ToEntity(this resenasDataModel model) => new()
        {
            id = model.Id,
            cliente_id = model.ClienteId,
            alojamiento_id = model.AlojamientoId,
            estrellas = model.Estrellas,
            comentario = model.Comentario
        };
    }
}
