using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IreservasDataService
    {
        Task<Guid> CreateReservaAsync(reservasDataModel model);
        Task<reservasDataModel?> GetDetailByIdAsync(Guid id);
        Task<DataPagedResult<reservasDataModel>> GetHistorialPagedAsync(Guid usuarioId, bool esAdmin, int page, int size);
        Task<bool> CambiarEstadoAsync(Guid reservaId, string nuevoEstado);
    }
}
