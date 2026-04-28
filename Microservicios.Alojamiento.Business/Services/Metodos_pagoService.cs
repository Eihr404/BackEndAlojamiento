using Microservicios.Alojamiento.Business.DTOs.Metodos_pago;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    // Cuidado aquí: asegúrate de que tu interfaz se llame IMetodos_pagoService o IMetodosPagoService
    public class Metodos_pagoService : IMetodos_pagoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Metodos_pagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Metodo_pagoResponse>> ObtenerTodosAsync()
        {
            var entities = await _unitOfWork.Metodos_pagoQueryRepository.GetAllAsync();
            return entities.Select(e => Metodos_pagoBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<Metodo_pagoResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Metodos_pagoQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el método de pago con ID: {id}");

            return Metodos_pagoBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<Metodo_pagoResponse> CrearMetodoPagoAsync(CrearMetodo_pagoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre del método de pago es obligatorio.");

            var dataModel = Metodos_pagoBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            entity.nombre = request.Nombre; // Asignación de la propiedad en minúsculas

            await _unitOfWork.Metodos_pagoRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Metodos_pagoBusinessMapper.ToResponse(dataModel);
        }

        public async Task<Metodo_pagoResponse> ActualizarMetodoPagoAsync(ActualizarMetodos_pagoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre del método de pago no puede estar vacío.");

            var entity = await _unitOfWork.Metodos_pagoQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Método de pago no encontrado.");

            entity.nombre = request.Nombre;

            _unitOfWork.Metodos_pagoRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Metodos_pagoBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarMetodoPagoAsync(Guid id)
        {
            var entity = await _unitOfWork.Metodos_pagoQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el método de pago con ID: {id}");

            _unitOfWork.Metodos_pagoRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}