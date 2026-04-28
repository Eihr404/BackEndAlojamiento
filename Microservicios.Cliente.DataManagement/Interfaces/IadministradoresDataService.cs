using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IadministradoresDataService
    {
        Task<administradoresDataModel?> GetByUsuarioIdAsync(Guid usuarioId);
        Task<administradoresDataModel?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(administradoresDataModel model);
    }
}
