using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class rolesRepository : RepositoryBase<roles>, IrolesRepository
    {
        public rolesRepository(AlojamientoDbContext context) : base(context) { }
    }
}
