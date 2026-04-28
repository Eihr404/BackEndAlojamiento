using Microservicios.Alojamiento.Business.DTOs.Reserva_detalles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataAccess.Entities; // Para el Enum TipoItemDetalle
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class Reserva_detallesService : IReserva_detallesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Reserva_detallesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Renombrado a: ObtenerDetallesPorReservaAsync
        public async Task<IEnumerable<Reserva_detalleResponse>> ObtenerDetallesPorReservaAsync(Guid reservaId)
        {
            var entities = await _unitOfWork.Reserva_detallesQueryRepository.GetDetallesByReservaAsync(reservaId);
            return entities.Select(e => Reserva_detallesBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<Reserva_detalleResponse?> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Reserva_detallesQueryRepository.ObtenerPorIdAsync(id);
            return entity != null ? Reserva_detallesBusinessMapper.ToResponse(entity.ToModel()) : null;
        }

        // ✅ Renombrado a: AgregarDetalleReservaAsync
        public async Task<Reserva_detalleResponse> AgregarDetalleReservaAsync(CrearReserva_detallesRequest request)
        {
            if (request.Cantidad <= 0)
                throw new ValidationException("La cantidad debe ser al menos 1.");

            if (request.PrecioCapturado < 0)
                throw new ValidationException("El precio no puede ser negativo.");

            if (string.IsNullOrWhiteSpace(request.TipoItem))
                throw new ValidationException("El tipo de ítem es obligatorio.");

            var dataModel = Reserva_detallesBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();

            entity.reserva_id = request.ReservaId;
            entity.item_id = request.ItemId;
            // ✅ Asignación directa como string para evitar errores de tipo en Postgres
            entity.tipo_item = request.TipoItem;
            entity.cantidad = request.Cantidad;
            entity.precio_capturado = request.PrecioCapturado;

            await _unitOfWork.Reserva_detallesRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Reserva_detallesBusinessMapper.ToResponse(dataModel);
        }

        // ✅ Renombrado a: ActualizarDetalleReservaAsync
        public async Task<Reserva_detalleResponse> ActualizarDetalleReservaAsync(ActualizarReserva_detallesRequest request)
        {
            if (request.Cantidad <= 0)
                throw new ValidationException("La cantidad debe ser al menos 1.");

            var entity = await _unitOfWork.Reserva_detallesQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Detalle de reserva no encontrado.");

            // Generalmente en un detalle de reserva solo se permite actualizar la cantidad o el precio, no a qué reserva pertenece
            entity.cantidad = request.Cantidad;
            entity.precio_capturado = request.PrecioCapturado;

            _unitOfWork.Reserva_detallesRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Reserva_detallesBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ Renombrado a: EliminarDetalleReservaAsync
        public async Task EliminarDetalleReservaAsync(Guid id)
        {
            var entity = await _unitOfWork.Reserva_detallesQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el detalle de reserva con ID: {id}");

            _unitOfWork.Reserva_detallesRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}