using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IclientesDataService
    {
        Task<clientesDataModel?> GetByUsuarioIdAsync(Guid usuarioId);
        Task<clientesDataModel?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(clientesDataModel model);
    }
}
