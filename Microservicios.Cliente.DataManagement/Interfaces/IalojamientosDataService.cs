using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IalojamientosDataService
    {
        Task<DataPagedResult<alojamientosDataModel>> GetSearchPagedAsync(alojamientosFiltroDataModel filtro);
        Task<alojamientosDataModel?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(alojamientosDataModel model);
        Task<bool> UpdateAsync(alojamientosDataModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}