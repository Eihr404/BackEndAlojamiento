using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface Iservicios_alojamientoDataService
    {
        Task<IEnumerable<servicios_alojamientoDataModel>> GetByAlojamientoAsync(Guid alojamientoId);
        Task<bool> AssignServicioToAlojamientoAsync(servicios_alojamientoDataModel model);
        Task<bool> RemoveServicioFromAlojamientoAsync(Guid alojamientoId, Guid servicioId);
        Task<bool> UpdatePrecioServicioAsync(Guid alojamientoId, Guid servicioId, decimal nuevoPrecio);
    }
}
