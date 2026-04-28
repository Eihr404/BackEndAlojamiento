using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IfacturasDataService
    {
        Task<facturasDataModel?> GetByReservaIdAsync(Guid reservaId);
        Task<string> GenerarFacturaAsync(Guid reservaId, Guid metodoPagoId);
    }
}
