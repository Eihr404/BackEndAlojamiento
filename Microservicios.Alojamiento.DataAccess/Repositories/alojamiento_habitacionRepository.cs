using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class alojamiento_habitacionRepository : RepositoryBase<alojamiento_habitacion>, Ialojamiento_habitacionRepository
    {
        public alojamiento_habitacionRepository(AlojamientoDbContext context) : base(context) { }
    }
}
