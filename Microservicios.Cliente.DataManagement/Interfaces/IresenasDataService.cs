using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Models;


namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IresenasDataService
    {
        Task<DataPagedResult<resenasDataModel>> GetByAlojamientoPagedAsync(Guid alojamientoId, int page, int size);
        Task<Guid> CreateAsync(resenasDataModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<decimal> GetPromedioEstrellasAsync(Guid alojamientoId);
    }
}