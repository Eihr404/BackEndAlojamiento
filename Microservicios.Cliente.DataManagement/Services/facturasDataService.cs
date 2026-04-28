using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class facturasDataService : IfacturasDataService
    {
        private readonly IUnitOfWork _uow;

        public facturasDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<facturasDataModel?> GetByReservaIdAsync(Guid reservaId)
        {
            var entity = await _uow.FacturasRepository.FirstOrDefaultAsync(f => f.reserva_id == reservaId);
            return entity?.ToModel();
        }

        public async Task<string> GenerarFacturaAsync(Guid reservaId, Guid metodoPagoId)
        {
            var factura = new facturas
            {
                id = Guid.NewGuid(),
                reserva_id = reservaId,
                metodo_pago_id = metodoPagoId,
                num_factura = $"FAC-{DateTime.Now.Ticks}", // Lógica simple de numeración
                fecha_emision = DateTime.UtcNow,
                estado_pago = "Existoso"
            };

            await _uow.FacturasRepository.AddAsync(factura);
            await _uow.SaveChangesAsync();
            return factura.num_factura;
        }
    }
}