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
    public class usuario_rolesDataService : Iusuario_rolesDataService
    {
        private readonly IUnitOfWork _uow;
        public usuario_rolesDataService(IUnitOfWork uow) => _uow = uow;

        // --- MÉTODO FALTANTE: Validar si un usuario tiene un rol por nombre ---
        public async Task<bool> UserHasRolAsync(Guid usuarioId, string nombreRol)
        {
            // Buscamos si existe la relación haciendo uso de la propiedad de navegación 'rol'
            var entity = await _uow.Usuario_rolesRepository.FirstOrDefaultAsync(
                ur => ur.usuario_id == usuarioId && ur.rol.nombre == nombreRol);

            // Si encontró algo, retorna true; si es null, retorna false
            return entity != null;
        }

        // --- Obtener los roles de un usuario específico ---
        public async Task<IEnumerable<usuario_rolesDataModel>> GetRolesByUsuarioIdAsync(Guid usuarioId)
        {
            // Usamos FindAsync para buscar en la base de datos
            var entities = await _uow.Usuario_rolesRepository.FindAsync(ur => ur.usuario_id == usuarioId);

            // Transformamos a Model y usamos .ToList() para ejecución inmediata
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<bool> AssignAsync(usuario_rolesDataModel model)
        {
            await _uow.Usuario_rolesRepository.AddAsync(model.ToEntity());
            return await _uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> RevokeAsync(Guid usuarioId, Guid rolId)
        {
            var entity = await _uow.Usuario_rolesRepository.FirstOrDefaultAsync(ur => ur.usuario_id == usuarioId && ur.rol_id == rolId);
            if (entity == null) return false;

            _uow.Usuario_rolesRepository.Delete(entity);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}