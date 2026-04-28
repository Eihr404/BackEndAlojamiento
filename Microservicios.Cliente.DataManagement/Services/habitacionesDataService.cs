using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;
namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class habitacionesDataService : IhabitacionesDataService
    {
        private readonly IUnitOfWork _uow;
        public habitacionesDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<habitacionesDataModel>> GetTiposHabitacionAsync()
            => (await _uow.HabitacionesRepository.GetAllAsync()).Select(x => x.ToModel());

        // Estos métodos suelen ser usados por el Admin para gestionar el catálogo global
        public async Task<IEnumerable<alojamiento_habitacionesDataModel>> GetConfiguracionByAlojamientoAsync(Guid alojamientoId)
            => (await _uow.Alojamiento_habitacionRepository.FindAsync(x => x.alojamiento_id == alojamientoId)).Select(x => x.ToModel());

        public async Task<bool> SaveConfiguracionAsync(alojamiento_habitacionesDataModel model)
        {
            var entity = model.ToEntity();
            await _uow.Alojamiento_habitacionRepository.AddAsync(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}
