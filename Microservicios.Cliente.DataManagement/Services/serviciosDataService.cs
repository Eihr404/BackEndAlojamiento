using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Models;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataAccess.Entities; // VITAL para poder instanciar 'usuario_roles'
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class serviciosDataService : IserviciosDataService
    {
        private readonly IUnitOfWork _uow;
        public serviciosDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<serviciosDataModel>> GetAllAsync()
            => (await _uow.ServiciosRepository.GetAllAsync()).Select(x => x.ToModel());

        public async Task<serviciosDataModel?> GetByIdAsync(Guid id)
            => (await _uow.ServiciosRepository.GetByIdAsync(id))?.ToModel();

        public async Task<Guid> CreateAsync(serviciosDataModel model)
        {
            var entity = model.ToEntity();
            await _uow.ServiciosRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.id;
        }

        public async Task<bool> UpdateAsync(serviciosDataModel model)
        {
            var entity = await _uow.ServiciosRepository.GetByIdAsync(model.Id);
            if (entity == null) return false;
            entity.nombre = model.Nombre;
            entity.descripcion = model.Descripcion;
            _uow.ServiciosRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.ServiciosRepository.GetByIdAsync(id);
            if (entity == null) return false;
            _uow.ServiciosRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}
