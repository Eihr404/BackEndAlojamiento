using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IserviciosDataService
    {
        Task<IEnumerable<serviciosDataModel>> GetAllAsync();
        Task<serviciosDataModel?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(serviciosDataModel model);
        Task<bool> UpdateAsync(serviciosDataModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}