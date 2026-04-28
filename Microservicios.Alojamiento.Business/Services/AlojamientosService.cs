using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Alojamientos;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers; // IMPORTANTE para .ToModel() y .ToEntity()
using Microservicios.Alojamiento.DataAccess.Common;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;



namespace Microservicios.Alojamiento.Business.Services
{
    public class AlojamientosService : IAlojamientosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CrearAlojamientoRequest> _crearValidator;

        public AlojamientosService(IUnitOfWork unitOfWork, IValidator<CrearAlojamientoRequest> crearValidator)
        {
            _unitOfWork = unitOfWork;
            _crearValidator = crearValidator;
        }

        public async Task<AlojamientoResponse> ObtenerPorIdAsync(Guid id) // Quitamos el '?'
        {
            var entity = await _unitOfWork.AlojamientosQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el alojamiento con ID: {id}");

            return AlojamientosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<PagedResult<AlojamientoResponse>> ObtenerFiltradosAsync(AlojamientoFiltroRequest filtro)
        {
            var pagedEntities = await _unitOfWork.AlojamientosQueryRepository.GetPagedSearchAsync(
                filtro.Ciudad,
                null,
                filtro.Pagina,
                filtro.RegistrosPorPagina,
                filtro.Tipo,
                filtro.AdminId,
                filtro.TieneWifi,        // ← agregar
                filtro.TienePiscina,     // ← agregar
                filtro.AdmiteMascotas,   // ← agregar
                filtro.TieneCocina);     // ← agregar

            var mappedItems = pagedEntities.Items
                .Select(e => AlojamientosBusinessMapper.ToResponse(e.ToModel()))
                .ToList();

            return new PagedResult<AlojamientoResponse>(
                mappedItems,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize);
        }

        public async Task<AlojamientoResponse> CrearAlojamientoAsync(CrearAlojamientoRequest request)
        {
            var validationResult = await _crearValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new ValidationException("Errores de validación en el alojamiento", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var dataModel = AlojamientosBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            await _unitOfWork.AlojamientosRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return AlojamientosBusinessMapper.ToResponse(dataModel);
        }

        public async Task<AlojamientoResponse> ActualizarAlojamientoAsync(ActualizarAlojamientoRequest request)
        {
            var entity = await _unitOfWork.AlojamientosQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el alojamiento con ID: {request.Id}");

            entity.nombre = request.Nombre;
            entity.ciudad = request.Ciudad;
            entity.direccion = request.Direccion;
            entity.tiene_wifi = request.TieneWifi;
            entity.tiene_piscina = request.TienePiscina;
            entity.admite_mascotas = request.AdmiteMascotas;
            entity.check_in = request.CheckIn;
            entity.check_out = request.CheckOut;

            _unitOfWork.AlojamientosRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return AlojamientosBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarAlojamientoAsync(Guid id)
        {
            var entity = await _unitOfWork.AlojamientosQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el alojamiento con ID: {id}");

            _unitOfWork.AlojamientosRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}