using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class reservasDataService : IreservasDataService
    {
        private readonly IUnitOfWork _uow;

        public reservasDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateReservaAsync(reservasDataModel model)
        {
            var entity = model.ToEntity();
            entity.fecha_solicitud = DateTime.UtcNow;

            if (model.Detalles != null)
            {
                entity.detalles = model.Detalles.Select(d => d.ToEntity()).ToList();
            }

            await _uow.ReservasRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();

            return entity.id;
        }

        public async Task<reservasDataModel?> GetDetailByIdAsync(Guid id)
        {
            var entity = await _uow.ReservasRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task<bool> CambiarEstadoAsync(Guid reservaId, string nuevoEstado)
        {
            // 1. Buscamos la entidad
            var entity = await _uow.ReservasRepository.GetByIdAsync(reservaId);
            if (entity == null) return false;

            // 2. ✅ CAMBIO: Asignación directa de string. 
            // Ya no necesitamos Enum.Parse, evitando errores de compilación y de tipo en BD.
            entity.estado = nuevoEstado;

            // 3. Actualizamos y persistimos
            _uow.ReservasRepository.Update(entity);

            return await _uow.SaveChangesAsync() > 0;
        }

        // --- EL MÉTODO CORREGIDO ---
        public async Task<DataPagedResult<reservasDataModel>> GetHistorialPagedAsync(Guid usuarioId, bool esAdmin, int page, int size)
        {
            // 1. Usamos FindAsync para filtrar
            // ⚠️ OJO: Ajusta "alojamiento.admin_id" o "usuario_id" si en tu entidad 'reservas' se llaman diferente (ej. cliente_id)
            var reservas = esAdmin
                ? await _uow.ReservasRepository.FindAsync(r => r.alojamiento.admin_id == usuarioId)
                : await _uow.ReservasRepository.FindAsync(r => r.cliente_id == usuarioId);

            // 2. Calculamos el total antes de cortar la lista
            var total = reservas.Count();

            // 3. Paginamos (Skip y Take) y transformamos a Model de forma segura con .ToList()
            var pagedItems = reservas
                .Skip((page - 1) * size)
                .Take(size)
                .Select(r => r.ToModel())
                .ToList();

            // 4. Retornamos usando los nombres en inglés de tu clase DataPagedResult
            return new DataPagedResult<reservasDataModel>
            {
                Items = pagedItems,
                TotalCount = total,
                CurrentPage = page,
                PageSize = size
            };
        }
    }
}