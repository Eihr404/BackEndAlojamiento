using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IhabitacionesDataService
    {
        Task<IEnumerable<habitacionesDataModel>> GetTiposHabitacionAsync();
        Task<IEnumerable<alojamiento_habitacionesDataModel>> GetConfiguracionByAlojamientoAsync(Guid alojamientoId);
        Task<bool> SaveConfiguracionAsync(alojamiento_habitacionesDataModel model);
    }
}
