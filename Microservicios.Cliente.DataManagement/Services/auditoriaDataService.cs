using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Models; // VITAL para reconocer auditoriaDataModel
using Microservicios.Alojamiento.DataManagement.Mappers; // VITAL para usar ToEntity() y ToModel()
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class auditoriaDataService : IauditoriaDataService
    {
        private readonly IUnitOfWork _uow;

        public auditoriaDataService(IUnitOfWork uow) => _uow = uow;

        public async Task LogAsync(auditoriaDataModel model)
        {
            var entity = model.ToEntity();
            entity.fecha_hora = DateTime.UtcNow;
            await _uow.AuditoriaRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<auditoriaDataModel>> GetByRegistroIdAsync(Guid registroId, string tabla)
        {
            // Cambiado WhereAsync por FindAsync para coincidir con tu IRepositoryBase
            var entities = await _uow.AuditoriaRepository.FindAsync(a => a.registro_id == registroId && a.tabla_afectada == tabla);
            return entities.Select(a => a.ToModel());
        }

        public async Task<IEnumerable<auditoriaDataModel>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            // Cambiado WhereAsync por FindAsync para coincidir con tu IRepositoryBase
            var entities = await _uow.AuditoriaRepository.FindAsync(a => a.usuario_id == usuarioId);
            return entities.Select(a => a.ToModel());
        }
    }
}