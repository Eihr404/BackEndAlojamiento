using Microservicios.Alojamiento.Business.DTOs.Roles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;

namespace Microservicios.Alojamiento.Business.Services
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RolesResponse>> ObtenerRolesAsync()
        {
            var entities = await _unitOfWork.RolQueryRepository.GetAllRolesAsync();
            return entities.Select(e => RolesBusinessMapper.ToResponse(e.ToModel()));
        }

        public async Task<RolesResponse> ObtenerPorIdAsync(Guid id) // Sin el '?'
        {
            var entity = await _unitOfWork.RolQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el rol con ID: {id}");

            return RolesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<RolesResponse> CrearRolAsync(CrearRolesRequest request)
        {
            // Opcional pero recomendado: Validar que no exista ya un rol con ese nombre

            var dataModel = RolesBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            entity.nombre = request.Nombre;
            entity.descripcion = request.Descripcion;

            await _unitOfWork.RolRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return RolesBusinessMapper.ToResponse(dataModel);
        }

        public async Task<RolesResponse> ActualizarRolAsync(ActualizarRolesRequest request)
        {
            var entity = await _unitOfWork.RolQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el rol con ID: {request.Id}"); // Usando tu excepción

            entity.nombre = request.Nombre;
            entity.descripcion = request.Descripcion;

            _unitOfWork.RolRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return RolesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarRolAsync(Guid id)
        {
            var entity = await _unitOfWork.RolQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el rol con ID: {id}");

            _unitOfWork.RolRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            // Ya no retornamos true/false; si sale de este método sin lanzar excepción, es éxito.
        }
    }
}