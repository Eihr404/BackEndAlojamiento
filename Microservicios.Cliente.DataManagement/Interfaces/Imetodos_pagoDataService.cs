using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface Imetodos_pagoDataService
    {
        Task<IEnumerable<metodos_pagoDataModel>> GetAllActiveAsync();
    }
}
