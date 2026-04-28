using Microservicios.Alojamiento.DataManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Models; // VITAL para reconocer auditoriaDataModel
using Microservicios.Alojamiento.DataManagement.Mappers; // VITAL para usar ToEntity() y ToModel()
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class reserva_detallesDataService : Ireserva_detallesDataService
    {
        private readonly IUnitOfWork _uow;
        public reserva_detallesDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<reserva_detallesDataModel>> GetByReservaIdAsync(Guid reservaId)
        {
            var entities = await _uow.Reserva_detallesRepository.FindAsync(d => d.reserva_id == reservaId);
            return entities.Select(e => e.ToModel());
        }
    }
}
