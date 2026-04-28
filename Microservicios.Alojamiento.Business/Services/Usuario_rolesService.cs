using Microservicios.Alojamiento.Business.DTOs.Usuario_roles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers; // 👈 Agregado para los mappers
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers; // 👈 Agregado para los mappers
using Microservicios.Alojamiento.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class Usuario_rolesService : IUsuario_rolesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Usuario_rolesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Corregido: Ahora devuelve Task<IEnumerable<Usuario_rolesResponse>> usando Mappers
        public async Task<IEnumerable<Usuario_rolesResponse>> ObtenerRolesPorUsuarioAsync(Guid usuarioId)
        {
            var entities = await _unitOfWork.Usuario_rolesQueryRepository.GetRolesByUsuarioAsync(usuarioId);
            return entities.Select(e => Usuario_rolesBusinessMapper.ToResponse(e.ToModel()));
        }

        // ✅ Corregido: Ahora devuelve Task<Usuario_rolesResponse>
        public async Task<Usuario_rolesResponse> AsignarRolAUsuarioAsync(CrearUsuario_rolesRequest request)
        {
            var existe = await _unitOfWork.Usuario_rolesQueryRepository.ObtenerAsignacionAsync(request.UsuarioId, request.RolId);
            if (existe != null)
                throw new ValidationException("El usuario ya tiene este rol asignado.");

            // Usamos el flujo de mappers estándar
            var dataModel = Usuario_rolesBusinessMapper.ToDataModel(request);

            var entity = dataModel.ToEntity();

            // Aseguramos la asignación manual por si el mapper no las enlaza
            entity.usuario_id = request.UsuarioId;
            entity.rol_id = request.RolId;

            await _unitOfWork.Usuario_rolesRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Usuario_rolesBusinessMapper.ToResponse(dataModel);
        }

        // Este método queda igual porque la interfaz seguramente sí pide un bool (Task<bool>) para eliminar
        public async Task EliminarRolDeUsuarioAsync(Guid usuarioId, Guid rolId)
        {
            var entity = await _unitOfWork.Usuario_rolesQueryRepository.ObtenerAsignacionAsync(usuarioId, rolId);
            if (entity == null)
                throw new NotFoundException($"No se encontró la asignación del rol {rolId} para el usuario {usuarioId}.");

            _unitOfWork.Usuario_rolesRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            // Éxito implícito por terminación normal
        }
    }
}