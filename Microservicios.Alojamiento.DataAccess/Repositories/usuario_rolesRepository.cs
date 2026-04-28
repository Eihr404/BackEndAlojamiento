using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class usuario_rolesRepository : RepositoryBase<usuario_roles>, Iusuario_rolesRepository
    {
        public usuario_rolesRepository(AlojamientoDbContext context) : base(context) { }
    }
}
