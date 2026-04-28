using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class clientesDataService : IclientesDataService
    {
        private readonly IUnitOfWork _uow;

        public clientesDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<clientesDataModel?> GetByUsuarioIdAsync(Guid usuarioId)
        {
            var entity = await _uow.ClientesRepository.FirstOrDefaultAsync(c => c.usuario_id == usuarioId);
            return entity?.ToModel();
        }

        public async Task<clientesDataModel?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.ClientesRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task<bool> UpdateAsync(clientesDataModel model)
        {
            var entity = await _uow.ClientesRepository.GetByIdAsync(model.Id);
            if (entity == null) return false;

            entity.nombre = model.Nombre;
            entity.apellido = model.Apellido;
            entity.telefono = model.Telefono;
            entity.documento_identidad = model.DocumentoIdentidad;

            _uow.ClientesRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}