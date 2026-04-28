using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;

namespace Microservicios.Alojamiento.Business.Services
{
    public class Alojamiento_habitacionService : IAlojamiento_habitacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Alojamiento_habitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }          


        // 3. Corregido: ObtenerHabitacionesPorAlojamientoAsync
        public async Task<IEnumerable<Alojamiento_habitacionResponse>> ObtenerHabitacionesPorAlojamientoAsync(Guid alojamientoId)
        {
            // Llamamos al QueryRepository que ya tiene la lógica de filtrado por alojamiento
            var entities = await _unitOfWork.Alojamiento_habitacionQueryRepository.GetPreciosByAlojamientoAsync(alojamientoId);
            if (entities == null)
                throw new NotFoundException($"No se encontró la configuración con ID: {alojamientoId}");

            return entities.Select(e => Alojamiento_habitacionBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<Alojamiento_habitacionResponse> AsignarHabitacionAAlojamientoAsync(CrearAlojamiento_habitacionRequest request)
        {
            // 1. Mapeo DTO -> DataModel
            var dataModel = Alojamiento_habitacionBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            // 2. Persistencia (Model -> Entity)
            var entity = dataModel.ToEntity();

            // Usamos los nombres de campos de tu entidad (snake_case)
            entity.alojamiento_id = request.AlojamientoId;
            entity.habitacion_id = request.HabitacionId;
            entity.precio_noche = request.PrecioNoche;
            entity.cantidad_total = request.CantidadTotal;

            await _unitOfWork.Alojamiento_habitacionRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Alojamiento_habitacionBusinessMapper.ToResponse(dataModel);
        }

        public async Task<IEnumerable<Alojamiento_habitacionResponse>> ObtenerPreciosPorAlojamientoAsync(Guid alojamientoId)
        {
            // ✅ Llamada a tu Query: GetPreciosByAlojamientoAsync
            var alojamiento = await _unitOfWork.AlojamientosQueryRepository.ObtenerPorIdAsync(alojamientoId);
            if (alojamiento == null)
                throw new NotFoundException($"El alojamiento con ID {alojamientoId} no existe.");

            // 2. Obtener las habitaciones
            var entities = await _unitOfWork.Alojamiento_habitacionQueryRepository.GetPreciosByAlojamientoAsync(alojamientoId);

            return entities.Select(e => Alojamiento_habitacionBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<Alojamiento_habitacionResponse> ActualizarPrecioOCantidadAsync(ActualizarAlojamiento_habitacionRequest request)
        {
            var entity = await _unitOfWork.Alojamiento_habitacionQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la reserva con ID: {request.Id}");

            entity.precio_noche = request.PrecioNoche;
            entity.cantidad_total = request.CantidadTotal;

            _unitOfWork.Alojamiento_habitacionRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Alojamiento_habitacionBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarRelacionAsync(Guid id)
        {
            var entity = await _unitOfWork.Alojamiento_habitacionQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la relación con ID: {id}");

            _unitOfWork.Alojamiento_habitacionRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}