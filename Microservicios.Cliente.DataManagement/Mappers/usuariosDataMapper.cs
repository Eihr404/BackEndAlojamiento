using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class usuariosDataMapper
    {
        public static usuariosDataModel ToModel(this usuarios entity) => new()
        {
            Id = entity.id,
            Email = entity.email,
            Activo = entity.activo,
            FechaCreacion = entity.fecha_creacion,
            Roles = entity.usuario_roles?.Select(ur => ur.rol.nombre).ToList() ?? new List<string>()
        };

        public static usuarios ToEntity(this usuariosDataModel model) => new()
        {
            id = model.Id,
            email = model.Email,
            activo = model.Activo
            // El password se maneja usualmente por separado por seguridad
        };
    }
}
