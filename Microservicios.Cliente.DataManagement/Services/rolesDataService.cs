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
    public class rolesDataService : IrolesDataService
    {
        private readonly IUnitOfWork _uow;
        public rolesDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<rolesDataModel>> GetAllAsync()
            => (await _uow.RolRepository.GetAllAsync()).Select(x => x.ToModel());

        public async Task<rolesDataModel?> GetByNameAsync(string nombreRol)
        {
            var entity = await _uow.RolRepository.FirstOrDefaultAsync(r => r.nombre == nombreRol);
            return entity?.ToModel();
        }

        public async Task<IEnumerable<string>> GetRolesByUsuarioAsync(Guid usuarioId)
        {
            var relaciones = await _uow.Usuario_rolesRepository.FindAsync(ur => ur.usuario_id == usuarioId);
            return relaciones.Select(r => r.rol?.nombre ?? string.Empty);
        }

        // --- MÉTODO FALTANTE 1: Asignar Rol ---
        public async Task<bool> AssignRolToUsuarioAsync(Guid usuarioId, Guid rolId)
        {
            // Primero verificamos que no tenga ya el rol asignado para evitar duplicados
            var existe = await _uow.Usuario_rolesRepository.FirstOrDefaultAsync(ur => ur.usuario_id == usuarioId && ur.rol_id == rolId);
            if (existe != null) return true; // Si ya lo tiene, decimos que fue un éxito

            var nuevaRelacion = new usuario_roles
            {
                usuario_id = usuarioId,
                rol_id = rolId
            };

            await _uow.Usuario_rolesRepository.AddAsync(nuevaRelacion);
            return await _uow.SaveChangesAsync() > 0;
        }

        // --- MÉTODO FALTANTE 2: Quitar Rol (Probablemente también te lo exija la interfaz) ---
        public async Task<bool> RemoveRolFromUsuarioAsync(Guid usuarioId, Guid rolId)
        {
            var relacion = await _uow.Usuario_rolesRepository.FirstOrDefaultAsync(ur => ur.usuario_id == usuarioId && ur.rol_id == rolId);
            if (relacion == null) return false; // No lo tenía asignado

            _uow.Usuario_rolesRepository.Delete(relacion);
            return await _uow.SaveChangesAsync() > 0;
        }
    }
}