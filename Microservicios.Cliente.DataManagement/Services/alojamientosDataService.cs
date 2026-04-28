using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class alojamientosDataService : IalojamientosDataService
    {
        private readonly IUnitOfWork _uow;

        public alojamientosDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DataPagedResult<alojamientosDataModel>> GetSearchPagedAsync(alojamientosFiltroDataModel filtro)
        {
            // Usamos el repositorio especializado de alojamientos
            var pagedEntities = await _uow.AlojamientosRepository.GetSearchPagedAsync(
                filtro.Ciudad,
                filtro.PrecioMaximo,
                filtro.TipoAlojamiento,
                filtro.Pagina,
                filtro.RegistrosPorPagina);

            return new DataPagedResult<alojamientosDataModel>
            {
                Items = pagedEntities.Items.Select(e => e.ToModel()).ToList(),
                TotalCount = pagedEntities.TotalCount,
                CurrentPage = pagedEntities.CurrentPage,
                PageSize = pagedEntities.PageSize
            };
        }

        public async Task<alojamientosDataModel?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.AlojamientosRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task<Guid> CreateAsync(alojamientosDataModel model)
        {
            var entity = model.ToEntity();
            await _uow.AlojamientosRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.id;
        }

        public async Task<bool> UpdateAsync(alojamientosDataModel model)
        {
            var entity = await _uow.AlojamientosRepository.GetByIdAsync(model.Id);
            if (entity == null) return false;

            // Actualizamos solo los campos permitidos
            entity.nombre = model.Nombre;
            entity.ciudad = model.Ciudad;
            entity.direccion = model.Direccion;
            entity.tiene_wifi = model.TieneWifi;
            entity.tiene_piscina = model.TienePiscina;
            entity.admite_mascotas = model.AdmiteMascotas;
            entity.check_in = model.CheckIn;
            entity.check_out = model.CheckOut;

            _uow.AlojamientosRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.AlojamientosRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.AlojamientosRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}