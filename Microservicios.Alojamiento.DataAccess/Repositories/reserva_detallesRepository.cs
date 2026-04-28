using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Repositories
{
    public class reserva_detallesRepository : RepositoryBase<reserva_detalles>, Ireserva_detallesRepository
    {
        public reserva_detallesRepository(AlojamientoDbContext context) : base(context) { }
    }
}
