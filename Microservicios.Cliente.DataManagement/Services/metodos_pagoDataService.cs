using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using Microservicios.Alojamiento.DataManagement.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.DataManagement.Services
{
    public class metodos_pagoDataService : Imetodos_pagoDataService
    {
        private readonly IUnitOfWork _uow;
        public metodos_pagoDataService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<metodos_pagoDataModel>> GetAllActiveAsync()
            => (await _uow.Metodos_pagoRepository.GetAllAsync()).Select(x => x.ToModel());
    }
}
