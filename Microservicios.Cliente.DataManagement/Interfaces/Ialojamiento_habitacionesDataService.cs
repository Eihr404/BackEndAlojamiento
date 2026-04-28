using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface Ialojamiento_habitacionesDataService
    {
        // Obtener todas las habitaciones configuradas para un alojamiento específico
        Task<IEnumerable<alojamiento_habitacionesDataModel>> GetByAlojamientoIdAsync(Guid alojamientoId);

        // Obtener el detalle específico de un tipo de habitación en un hotel
        Task<alojamiento_habitacionesDataModel?> GetSpecificAsync(Guid alojamientoId, Guid habitacionId);

        // Agregar un tipo de habitación a un hotel o actualizar su precio/stock
        Task<bool> UpsertAsync(alojamiento_habitacionesDataModel model);

        // Quitar un tipo de habitación de la oferta de un hotel
        Task<bool> RemoveAsync(Guid alojamientoId, Guid habitacionId);

        // Actualizar solo el stock (útil para el proceso de reservas)
        Task<bool> UpdateStockAsync(Guid alojamientoId, Guid habitacionId, int nuevaCantidad);
    }
}