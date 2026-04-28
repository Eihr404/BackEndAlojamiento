using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Habitaciones;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers; // Para .ToModel() y .ToEntity()
using Microservicios.Alojamiento.DataAccess.Common;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class HabitacionesService : IHabitacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CrearHabitacionesRequest> _crearValidator;

        // Dentro de HabitacionesService.cs

        public async Task<IEnumerable<HabitacionesResponse>> ObtenerTodasAsync()
        {
            // 1. Lectura: Pedimos todas las entidades al QueryRepository
            // Nota: Si prefieres usar GetTiposDisponiblesAsync, cámbialo aquí
            var entities = await _unitOfWork.HabitacionesQueryRepository.GetAllAsync();

            // 2. Mapeo: Entity -> Model -> Response
            // Usamos el .Select para transformar toda la lista
            return entities.Select(e => HabitacionesBusinessMapper.ToResponse(e.ToModel()));
        }

        public HabitacionesService(IUnitOfWork unitOfWork, IValidator<CrearHabitacionesRequest> crearValidator)
        {
            _unitOfWork = unitOfWork;
            _crearValidator = crearValidator;
        }

        public async Task<HabitacionesResponse> CrearHabitacionAsync(CrearHabitacionesRequest request)
        {
            var validationResult = await _crearValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException("Errores de validación", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var dataModel = HabitacionesBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            await _unitOfWork.HabitacionesRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return HabitacionesBusinessMapper.ToResponse(dataModel);
        }

        public async Task<HabitacionesResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.HabitacionesQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la habitación con ID: {id}");

            return HabitacionesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<IEnumerable<HabitacionesResponse>> ObtenerPorAlojamientoAsync(Guid alojamientoId)
        {
            // Asumiendo que tu QueryRepository tiene este método para listar habitaciones de un hotel
            var entities = await _unitOfWork.HabitacionesQueryRepository.GetByAlojamientoIdAsync(alojamientoId);

            return entities.Select(e => HabitacionesBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<HabitacionesResponse> ActualizarHabitacionAsync(ActualizarHabitacionesRequest request)
        {
            var entity = await _unitOfWork.HabitacionesQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Habitación no encontrada.");

            // Actualización manual de campos (DataAccess usa snake_case usualmente)
            entity.nombre_tipo = request.NombreTipo;
            entity.capacidad_personas = request.CapacidadPersonas;
            entity.numero_camas = request.NumeroCamas;

            _unitOfWork.HabitacionesRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return HabitacionesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarHabitacionAsync(Guid id)
        {
            var entity = await _unitOfWork.HabitacionesQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la habitación con ID: {id}");

            _unitOfWork.HabitacionesRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}