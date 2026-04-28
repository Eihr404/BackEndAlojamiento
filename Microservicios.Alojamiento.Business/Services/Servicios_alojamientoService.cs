using Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class Servicios_alojamientoService : IServicios_alojamientoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Servicios_alojamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Corregido a: ObtenerServiciosPorAlojamientoAsync
        public async Task<IEnumerable<Servicios_alojamientoResponse>> ObtenerServiciosPorAlojamientoAsync(Guid alojamientoId)
        {
            var entities = await _unitOfWork.Servicios_alojamientoQueryRepository.GetServiciosActivosAsync(alojamientoId);
            return entities.Select(e => Servicios_alojamientoBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<Servicios_alojamientoResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Servicios_alojamientoQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la asignación de servicio con ID: {id}");

            return Servicios_alojamientoBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ Corregido a: AsignarServicioAAlojamientoAsync usando CrearServicios_alojamientoRequest
        public async Task<Servicios_alojamientoResponse> AsignarServicioAAlojamientoAsync(CrearServicios_alojamientoRequest request)
        {
            // Validación de negocio
            if (request.PrecioAdicional < 0)
                throw new ValidationException("El precio adicional no puede ser negativo.");

            var dataModel = Servicios_alojamientoBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();

            // Mapeo manual seguro
            entity.alojamiento_id = request.AlojamientoId;
            entity.servicio_id = request.ServicioId;
            entity.precio_adicional = request.PrecioAdicional;
            entity.esta_activo = true; // Por defecto lo activamos al asignarlo

            await _unitOfWork.Servicios_alojamientoRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Servicios_alojamientoBusinessMapper.ToResponse(dataModel);
        }

        // ✅ Corregido a: ActualizarServicioAsignadoAsync
        public async Task<Servicios_alojamientoResponse> ActualizarServicioAsignadoAsync(ActualizarServicios_alojamientoRequest request)
        {
            if (request.PrecioAdicional < 0)
                throw new ValidationException("El precio adicional no puede ser negativo.");

            var entity = await _unitOfWork.Servicios_alojamientoQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("El servicio asignado no existe.");

            // Solo permitimos actualizar el precio y el estado activo.
            entity.precio_adicional = request.PrecioAdicional;
            entity.esta_activo = request.EstaActivo;

            _unitOfWork.Servicios_alojamientoRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Servicios_alojamientoBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ Corregido a: QuitarServicioDeAlojamientoAsync
        public async Task QuitarServicioDeAlojamientoAsync(Guid id)
        {
            var entity = await _unitOfWork.Servicios_alojamientoQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la asignación de servicio con ID: {id}");

            _unitOfWork.Servicios_alojamientoRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}