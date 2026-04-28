using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Models; // VITAL para reconocer auditoriaDataModel
using Microservicios.Alojamiento.DataManagement.Mappers; // VITAL para usar ToEntity() y ToModel()
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class resenasDataService : IresenasDataService
    {
        private readonly IUnitOfWork _uow;

        public resenasDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<Guid> CreateAsync(resenasDataModel model)
        {
            var entity = model.ToEntity();
            entity.fecha = DateTime.UtcNow;
            await _uow.ResenasRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.id;
        }

        public async Task<DataPagedResult<resenasDataModel>> GetByAlojamientoPagedAsync(Guid alojamientoId, int page, int size)
        {
            // 1. Usamos FindAsync (que ya existe en tu IRepositoryBase) para traer SOLO las de este hotel
            var resenas = await _uow.ResenasRepository.FindAsync(x => x.alojamiento_id == alojamientoId);

            // 2. Calculamos el total de reseñas de este hotel
            var total = resenas.Count();

            // 3. Aplicamos la paginación (Skip y Take) y transformamos a Model
            var pagedItems = resenas
                .Skip((page - 1) * size)
                .Take(size)
                .Select(r => r.ToModel())
                .ToList();

            // 4. Retornamos usando las propiedades en inglés de tu DataPagedResult
            return new DataPagedResult<resenasDataModel>
            {
                Items = pagedItems,
                TotalCount = total,
                CurrentPage = page,
                PageSize = size
            };
        }

        public async Task<decimal> GetPromedioEstrellasAsync(Guid alojamientoId)
        {
            var resenas = await _uow.ResenasRepository.FindAsync(r => r.alojamiento_id == alojamientoId);
            if (!resenas.Any()) return 0;
            return (decimal)resenas.Average(r => r.estrellas);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.ResenasRepository.GetByIdAsync(id);
            if (entity == null) return false;
            _uow.ResenasRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}