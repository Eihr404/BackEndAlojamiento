using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface Ireserva_detallesDataService
    {
        Task<IEnumerable<reserva_detallesDataModel>> GetByReservaIdAsync(Guid reservaId);
    }
}
