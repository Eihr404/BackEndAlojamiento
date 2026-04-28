using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IauditoriaDataService
    {
        Task LogAsync(auditoriaDataModel model);
        Task<IEnumerable<auditoriaDataModel>> GetByRegistroIdAsync(Guid registroId, string tabla);
        Task<IEnumerable<auditoriaDataModel>> GetByUsuarioIdAsync(Guid usuarioId);
    }
}
