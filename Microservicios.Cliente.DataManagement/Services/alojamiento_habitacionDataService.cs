using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Models;
using Microservicios.Alojamiento.DataManagement.Mappers; // VITAL para el ToModel() y ToEntity()

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class alojamiento_habitacionesDataService : Ialojamiento_habitacionesDataService
    {
        private readonly IUnitOfWork _uow;

        public alojamiento_habitacionesDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<alojamiento_habitacionesDataModel>> GetByAlojamientoIdAsync(Guid alojamientoId)
        {
            var entities = await _uow.Alojamiento_habitacionRepository.FindAsync(x => x.alojamiento_id == alojamientoId);
            return entities.Select(e => e.ToModel());
        }

        // --- MÉTODO FALTANTE 1: Obtener el detalle específico ---
        public async Task<alojamiento_habitacionesDataModel?> GetSpecificAsync(Guid alojamientoId, Guid habitacionId)
        {
            var entity = await _uow.Alojamiento_habitacionRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == alojamientoId && x.habitacion_id == habitacionId);

            return entity?.ToModel();
        }

        public async Task<bool> UpsertAsync(alojamiento_habitacionesDataModel model)
        {
            var entity = await _uow.Alojamiento_habitacionRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == model.AlojamientoId && x.habitacion_id == model.HabitacionId);

            if (entity == null)
            {
                await _uow.Alojamiento_habitacionRepository.AddAsync(model.ToEntity());
            }
            else
            {
                entity.precio_noche = model.PrecioNoche;
                entity.cantidad_total = model.CantidadTotal;
                _uow.Alojamiento_habitacionRepository.Update(entity);
            }

            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockAsync(Guid alojamientoId, Guid habitacionId, int nuevaCantidad)
        {
            var entity = await _uow.Alojamiento_habitacionRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == alojamientoId && x.habitacion_id == habitacionId);

            if (entity == null) return false;

            entity.cantidad_total = nuevaCantidad;
            _uow.Alojamiento_habitacionRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        // --- MÉTODO FALTANTE 2: Eliminar la relación ---
        public async Task<bool> RemoveAsync(Guid alojamientoId, Guid habitacionId)
        {
            var entity = await _uow.Alojamiento_habitacionRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == alojamientoId && x.habitacion_id == habitacionId);

            if (entity == null) return false;

            _uow.Alojamiento_habitacionRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}