using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Reservas;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataAccess.Entities; // Necesario para EstadoReserva
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class ReservasService : IReservasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CrearReservaRequest> _validator;

        public async Task<IEnumerable<ReservaResponse>> ObtenerReservasPorClienteAsync(Guid clienteId)
        {
            var entities = await _unitOfWork.ReservasQueryRepository.GetByClienteIdAsync(clienteId);
                        
            return entities.Select(e => ReservasBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<IEnumerable<ReservaResponse>> ObtenerReservasPorAdminAsync(Guid adminId)
        {
            // Reutiliza GetHistorialPagedAsync con esAdmin=true
            var paged = await _unitOfWork.ReservasQueryRepository.GetHistorialPagedAsync(
                adminId,
                esAdmin: true,
                page: 1,
                size: 100);

            return paged.Items.Select(e => ReservasBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task CambiarEstadoReservaAsync(Guid id, string nuevoEstado)
        {
            var entity = await _unitOfWork.ReservasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la reserva con ID: {id}");

            // Validación simple: solo verificamos que no sea nulo
            if (string.IsNullOrWhiteSpace(nuevoEstado))
                throw new ValidationException("El estado de reserva no puede estar vacío.");

            entity.estado = nuevoEstado; // Asignación directa de string

            _unitOfWork.ReservasRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public ReservasService(IUnitOfWork unitOfWork, IValidator<CrearReservaRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ReservaResponse> CrearReservaAsync(CrearReservaRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException("Error en la validación de la reserva",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var dataModel = ReservasBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            entity.estado = "Pendiente";
            entity.fecha_solicitud = DateTime.UtcNow;

            await _unitOfWork.ReservasRepository.AddAsync(entity);

            // ← Guardar detalle con habitacionId
            if (request.HabitacionId != Guid.Empty)
            {
                var detalle = new reserva_detalles
                {
                    reserva_id = dataModel.Id,
                    item_id = request.HabitacionId, // ← guardamos el habitacionId
                    tipo_item = "Habitacion",
                    cantidad = 1,
                    precio_capturado = request.MontoTotal
                };
                await _unitOfWork.Reserva_detallesRepository.AddAsync(detalle);

                // Decrementar disponibilidad
                var relacion = await _unitOfWork.Alojamiento_habitacionQueryRepository
                    .GetByAlojamientoYHabitacionAsync(
                        request.AlojamientoId,
                        request.HabitacionId);

                if (relacion != null && relacion.cantidad_total > 0)
                {
                    relacion.cantidad_total--;
                    _unitOfWork.Alojamiento_habitacionRepository.Update(relacion);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return ReservasBusinessMapper.ToResponse(dataModel);
        }

        public async Task<ReservaResponse?> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ReservasQueryRepository.ObtenerPorIdAsync(id);
            return entity != null ? ReservasBusinessMapper.ToResponse(entity.ToModel()) : null;
        }

        public async Task<PagedResult<ReservaResponse>> ObtenerHistorialAsync(Guid usuarioId, bool esAdmin, int pagina, int registrosPorPagina)
        {
            // ✅ Sincronizado con tu método: GetHistorialPagedAsync
            var pagedEntities = await _unitOfWork.ReservasQueryRepository.GetHistorialPagedAsync(
                usuarioId,
                esAdmin,
                pagina,
                registrosPorPagina);

            var mappedItems = pagedEntities.Items
                .Select(e => ReservasBusinessMapper.ToResponse(e.ToModel()))
                .ToList();

            // ✅ Usando las propiedades reales de tu PagedResult: TotalCount y CurrentPage
            return new PagedResult<ReservaResponse>(
                mappedItems,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize);
        }

        public async Task CancelarReservaAsync(Guid id)
        {
            var entity = await _unitOfWork.ReservasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la reserva con ID: {id}");

            entity.estado = "Cancelada";
            _unitOfWork.ReservasRepository.Update(entity);

            // Restaurar disponibilidad
            var detalles = await _unitOfWork.Reserva_detallesQueryRepository
                .GetByReservaIdAsync(id);

            var detalle = detalles.FirstOrDefault(d => d.tipo_item == "Habitacion");
            if (detalle != null)
            {
                var relacion = await _unitOfWork.Alojamiento_habitacionQueryRepository
                    .GetByAlojamientoYHabitacionAsync(
                        entity.alojamiento_id,
                        detalle.item_id);

                if (relacion != null)
                {
                    relacion.cantidad_total++;
                    _unitOfWork.Alojamiento_habitacionRepository.Update(relacion);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}