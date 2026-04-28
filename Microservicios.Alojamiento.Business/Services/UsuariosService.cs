using Microservicios.Alojamiento.Business.DTOs.Usuarios; // Ajusta a tu namespace
using Microservicios.Alojamiento.Business.Exceptions;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuariosResponse> ObtenerPorIdAsync(Guid id) // Quitamos el '?'
        {
            var entity = await _unitOfWork.UsuarioQueryRepository.ObtenerPorIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró ningún usuario con el ID: {id}");
            }

            return UsuariosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<UsuariosResponse> ObtenerPorEmailAsync(string email) // Quitamos el '?'
        {
            var entity = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(email);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró ningún usuario con el correo: {email}");
            }

            return UsuariosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<UsuariosResponse> CrearUsuarioAsync(CrearUsuariosRequest request)
        {
            // Validar que el email no exista
            var existe = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(request.Email);
            if (existe != null)
                throw new ValidationException("El correo electrónico ya está en uso.");

            var dataModel = UsuariosBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();

            // Asignación explícita (snake_case según tu base de datos)
            entity.email = request.Email;
            entity.password_hash = request.Password; // Idealmente aquí harías un hash
            entity.activo = true; // Todo usuario nuevo nace activo por defecto
            entity.fecha_creacion = DateTime.UtcNow;

            await _unitOfWork.UsuarioRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return UsuariosBusinessMapper.ToResponse(dataModel);
        }

        public async Task<UsuariosResponse> ActualizarUsuarioAsync(ActualizarUsuariosRequest request)
        {
            var entity = await _unitOfWork.UsuarioQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Usuario no encontrado.");

            // Validar que si cambia el email, no choque con otro existente
            if (entity.email != request.Email)
            {
                var existe = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(request.Email);
                if (existe != null)
                    throw new ValidationException("El nuevo correo electrónico ya está en uso.");
            }

            entity.email = request.Email;
            entity.activo = request.Activo;

            // Nota: La contraseña suele actualizarse en un endpoint/método separado (ej. CambiarPasswordAsync)

            _unitOfWork.UsuarioRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return UsuariosBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ MÉTODO AÑADIDO: CambiarEstadoAsync
        public async Task CambiarEstadoAsync(Guid id, bool estado)
        {
            var entity = await _unitOfWork.UsuarioQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el usuario con ID: {id}");

            entity.activo = estado;
            _unitOfWork.UsuarioRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(Guid id)
        {
            var entity = await _unitOfWork.UsuarioQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el usuario con ID: {id}");

            _unitOfWork.UsuarioRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}