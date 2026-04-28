using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Mappers
{
    public static class usuario_rolesDataMapper
    {
        public static usuario_rolesDataModel ToModel(this usuario_roles entity) => new()
        {
            UsuarioId = entity.usuario_id,
            RolId = entity.rol_id,
            NombreRol = entity.rol?.nombre ?? string.Empty
        };

        public static usuario_roles ToEntity(this usuario_rolesDataModel model) => new()
        {
            usuario_id = model.UsuarioId,
            rol_id = model.RolId
        };
    }
}

