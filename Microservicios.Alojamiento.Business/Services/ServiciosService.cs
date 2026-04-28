using Microservicios.Alojamiento.Business.DTOs.Servicios; // Ajusta según tu namespace
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class ServiciosService : IServiciosService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiciosResponse>> ObtenerTodosAsync()
        {
            var entities = await _unitOfWork.ServiciosQueryRepository.GetAllMasterServicesAsync();
            return entities.Select(e => ServiciosBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<ServiciosResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ServiciosQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el servicio con ID: {id}");

            return ServiciosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<ServiciosResponse> CrearServicioAsync(CrearServiciosRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre del servicio es obligatorio.");

            var dataModel = ServiciosBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();

            // Asignación explícita (minúsculas por tu entidad)
            entity.nombre = request.Nombre;
            entity.descripcion = request.Descripcion;

            await _unitOfWork.ServiciosRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiciosBusinessMapper.ToResponse(dataModel);
        }

        public async Task<ServiciosResponse> ActualizarServicioAsync(ActualizarServiciosRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre del servicio no puede estar vacío.");

            var entity = await _unitOfWork.ServiciosQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Servicio no encontrado.");

            entity.nombre = request.Nombre;
            entity.descripcion = request.Descripcion;

            _unitOfWork.ServiciosRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiciosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarServicioAsync(Guid id)
        {
            var entity = await _unitOfWork.ServiciosQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el servicio con ID: {id}");

            _unitOfWork.ServiciosRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            // Exitoso por defecto (si no lanza excepción)
        }
    }
}