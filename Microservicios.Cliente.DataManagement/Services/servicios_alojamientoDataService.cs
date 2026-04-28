using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Models;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class servicios_alojamientoDataService : Iservicios_alojamientoDataService
    {
        private readonly IUnitOfWork _uow;

        public servicios_alojamientoDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<servicios_alojamientoDataModel>> GetByAlojamientoAsync(Guid alojamientoId)
        {
            // Usamos el repositorio para obtener la relación
            var entities = await _uow.Servicios_alojamientoRepository.FindAsync(x => x.alojamiento_id == alojamientoId);

            // Agregamos .ToList() para materializar en memoria de forma segura
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<bool> AssignServicioToAlojamientoAsync(servicios_alojamientoDataModel model)
        {
            var entity = model.ToEntity();
            await _uow.Servicios_alojamientoRepository.AddAsync(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePrecioServicioAsync(Guid alojamientoId, Guid servicioId, decimal nuevoPrecio)
        {
            var entity = await _uow.Servicios_alojamientoRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == alojamientoId && x.servicio_id == servicioId);

            if (entity == null) return false;

            entity.precio_adicional = nuevoPrecio;
            _uow.Servicios_alojamientoRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        // --- MÉTODO FALTANTE: Quitar el servicio del alojamiento ---
        public async Task<bool> RemoveServicioFromAlojamientoAsync(Guid alojamientoId, Guid servicioId)
        {
            // 1. Buscamos la relación específica
            var entity = await _uow.Servicios_alojamientoRepository.FirstOrDefaultAsync(
                x => x.alojamiento_id == alojamientoId && x.servicio_id == servicioId);

            // 2. Si no existe, retornamos falso
            if (entity == null) return false;

            // 3. Si existe, lo eliminamos usando el Unit of Work
            _uow.Servicios_alojamientoRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}