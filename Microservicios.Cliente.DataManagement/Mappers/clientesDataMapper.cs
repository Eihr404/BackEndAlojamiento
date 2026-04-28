using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class clienteDataMapper
    {
        public static clientesDataModel ToModel(this clientes entity) => new()
        {
            Id = entity.id,
            UsuarioId = entity.usuario_id,
            Nombre = entity.nombre,
            Apellido = entity.apellido,
            Telefono = entity.telefono,
            DocumentoIdentidad = entity.documento_identidad
        };

        public static clientes ToEntity(this clientesDataModel model) => new()
        {
            id = model.Id,
            usuario_id = model.UsuarioId,
            nombre = model.Nombre,
            apellido = model.Apellido,
            telefono = model.Telefono,
            documento_identidad = model.DocumentoIdentidad
        };
    }
}
