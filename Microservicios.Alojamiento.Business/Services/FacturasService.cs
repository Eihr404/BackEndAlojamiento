using Microservicios.Alojamiento.Business.DTOs.Facturas; // Ajusta según tu namespace
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataAccess.Entities; // Para el Enum EstadoFactura
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class FacturasService : IFacturasService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacturasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FacturaResponse> GenerarFacturaAsync(CrearFacturaRequest request)
        {
            // Generar número de factura automáticamente si no viene
            if (string.IsNullOrWhiteSpace(request.NumFactura))
            {
                var codigo = Guid.NewGuid().ToString("N")[..10].ToUpper();
                request.NumFactura = $"F-{codigo}"; // Ejemplo: F-A1B2C3D4E5 = 12 caracteres ✓
            }

            // resto del código sin cambios
            var dataModel = FacturasBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            entity.reserva_id = request.ReservaId;
            entity.num_factura = request.NumFactura;
            entity.metodo_pago_id = request.MetodoPagoId;
            entity.fecha_emision = DateTime.UtcNow;
            entity.estado_pago = "Exitoso";

            await _unitOfWork.FacturasRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return FacturasBusinessMapper.ToResponse(dataModel);
        }

        public async Task<FacturaResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.FacturasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la factura con ID: {id}");

            return FacturasBusinessMapper.ToResponse(entity.ToModel());
        }
        public async Task<FacturaResponse> ObtenerPorNumeroAsync(string numFactura)
        {
            var entity = await _unitOfWork.FacturasQueryRepository.GetFacturaConDetallesAsync(numFactura);
            if (entity == null)
                throw new NotFoundException($"No se encontró la factura número: {numFactura}");

            return FacturasBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ MÉTODO AÑADIDO: ObtenerFacturaPorReservaAsync
        public async Task<FacturaResponse> ObtenerFacturaPorReservaAsync(Guid reservaId)
        {
            var entity = await _unitOfWork.FacturasQueryRepository.ObtenerPorReservaAsync(reservaId);
            if (entity == null)
                throw new NotFoundException($"No se encontró factura para la reserva con ID: {reservaId}");

            return FacturasBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task ActualizarEstadoPagoAsync(Guid id, string nuevoEstado)
        {
            var entity = await _unitOfWork.FacturasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la factura con ID: {id}");

            // Validación sencilla: solo aceptamos valores no vacíos
            if (string.IsNullOrWhiteSpace(nuevoEstado))
                throw new ValidationException("El estado de pago no puede estar vacío.");

            // Guardamos directamente el string
            entity.estado_pago = nuevoEstado;

            _unitOfWork.FacturasRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}