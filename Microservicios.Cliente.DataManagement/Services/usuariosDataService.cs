using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class usuariosDataService : IusuariosDataService
    {
        private readonly IUnitOfWork _uow;

        public usuariosDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<usuariosDataModel?> GetByEmailAsync(string email)
        {
            // Buscamos en el repositorio de usuarios (asumiendo que tiene GetByEmail)
            var entity = await _uow.UsuarioRepository.FirstOrDefaultAsync(u => u.email == email);
            return entity?.ToModel();
        }

        public async Task<bool> RegistrarUsuarioAsync(usuariosDataModel model, string password)
        {
            var entity = model.ToEntity();
            entity.password_hash = password; // El hash se genera en la capa de Business
            entity.fecha_creacion = DateTime.UtcNow;

            await _uow.UsuarioRepository.AddAsync(entity);
            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePerfilClienteAsync(clientesDataModel model)
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

        public async Task<bool> UpdatePerfilAdminAsync(administradoresDataModel model)
        {
            var entity = await _uow.AdministradoresRepository.GetByIdAsync(model.Id);
            if (entity == null) return false;

            entity.nombre_comercial = model.NombreComercial;
            entity.nit_tax = model.NitTax;
            entity.telefono_soporte = model.TelefonoSoporte;

            _uow.AdministradoresRepository.Update(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}