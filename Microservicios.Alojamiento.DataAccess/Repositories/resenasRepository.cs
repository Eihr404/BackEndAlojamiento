using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class resenasRepository : RepositoryBase<resenas>, IresenasRepository
    {
        public resenasRepository(AlojamientoDbContext context) : base(context) { }
    }
}
