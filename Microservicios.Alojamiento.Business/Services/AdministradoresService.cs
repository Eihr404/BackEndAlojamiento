using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Administradores;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers; // .ToModel() y .ToEntity()
using Microservicios.Alojamiento.DataAccess.Common;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;
using Microservicios.Alojamiento.Business.Exceptions;

namespace Microservicios.Alojamiento.Business.Services
{
    public class AdministradoresService : IAdministradoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CrearAdministradoresRequest> _crearValidator;

        public async Task<AdministradoresResponse?> ObtenerPorUsuarioIdAsync(Guid usuarioId)
        {
            // 1. Lectura: Llamamos al QueryRepository que ya preparamos antes
            // Este método busca en la tabla 'administradores' usando el campo 'usuario_id'
            var entity = await _unitOfWork.AdministradoresQueryRepository.ObtenerPorUsuarioIdAsync(usuarioId);

            // 2. Mapeo: Entity -> Model -> Response
            // Si la entidad es nula (no es admin), devolvemos null
            return entity != null ? AdministradoresBusinessMapper.ToResponse(entity.ToModel()) : null;
        }
        public AdministradoresService(IUnitOfWork unitOfWork, IValidator<CrearAdministradoresRequest> crearValidator)
        {
            _unitOfWork = unitOfWork;
            _crearValidator = crearValidator;
        }

        public async Task<AdministradoresResponse> RegistrarAdministradorAsync(CrearAdministradoresRequest request)
        {
            var validationResult = await _crearValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                // Pasamos la lista de errores al constructor de tu ValidationException
                throw new ValidationException("Error de validación", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var dataModel = AdministradoresBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            await _unitOfWork.AdministradoresRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return AdministradoresBusinessMapper.ToResponse(dataModel);
        }

        public async Task<AdministradoresResponse> CrearAdministradorAsync(CrearAdministradoresRequest request)
        {
            // 1. Validar
            var validationResult = await _crearValidator.ValidateAsync(request);
            if (!validationResult.IsValid) 
                throw new ValidationException("Datos Incorrectos");

            // 2. Mapeo DTO -> DataModel
            var dataModel = AdministradoresBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            // 3. Persistencia (Model -> Entity)
            var entity = dataModel.ToEntity();
            await _unitOfWork.AdministradoresRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return AdministradoresBusinessMapper.ToResponse(dataModel);
        }

        public async Task<AdministradoresResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.AdministradoresQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el administrador con ID: {id}");

            return AdministradoresBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<AdministradoresResponse> ActualizarAdministradorAsync(ActualizarAdministradoresRequest request)
        {
            var entity = await _unitOfWork.AdministradoresQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el administrador con ID: {request.Id}");

            entity.nombre_comercial = request.NombreComercial;
            entity.nit_tax = request.NitTax;
            entity.telefono_soporte = request.TelefonoSoporte;

            _unitOfWork.AdministradoresRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return AdministradoresBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarAdministradorAsync(Guid id)
        {
            var entity = await _unitOfWork.AdministradoresQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el administrador con ID: {id}");

            var tieneAlojamientos = await _unitOfWork.AlojamientosQueryRepository
                    .ExistePorAdminIdAsync(id);

            if (tieneAlojamientos)
                throw new BusinessException("No se puede eliminar el administrador porque tiene alojamientos asociados");

            _unitOfWork.AdministradoresRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}