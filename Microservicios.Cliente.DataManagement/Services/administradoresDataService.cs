using Microservicios.Alojamiento.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class administradoresDataService : IadministradoresDataService
    {
        private readonly IUnitOfWork _uow;

        public administradoresDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<administradoresDataModel?> GetByUsuarioIdAsync(Guid usuarioId)
        {
            var entity = await _uow.AdministradoresRepository.FirstOrDefaultAsync(a => a.usuario_id == usuarioId);
            return entity?.ToModel();
        }

        public async Task<administradoresDataModel?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.AdministradoresRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task<bool> UpdateAsync(administradoresDataModel model)
        {
            var entity = await _uow.AdministradoresRepository.GetByIdAsync(model.Id);
            if (entity == null) return false;

            // Sincronizamos con los nombres reales de tu Entidad (DataAccess)
            entity.nombre_comercial = model.NombreComercial;
            entity.nit_tax = model.NitTax;
            entity.telefono_soporte = model.TelefonoSoporte;

            _uow.AdministradoresRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}