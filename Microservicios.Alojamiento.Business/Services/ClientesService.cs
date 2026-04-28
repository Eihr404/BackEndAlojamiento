using Microservicios.Alojamiento.Business.DTOs.Clientes; // Ajusta según tu namespace
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ClienteResponse>> ObtenerFiltradosAsync(ClienteFiltroRequest request)
        {
            // 1. Llamamos al QueryRepository pasando las propiedades exactas de tu DTO
            var pagedEntities = await _unitOfWork.ClientesQueryRepository.ObtenerFiltradosPagedAsync(
                request.Nombre,
                request.Apellido,
                request.DocumentoIdentidad,
                request.Pagina,
                request.RegistrosPorPagina
            );

            // 2. Mapeamos la lista de entidades a respuestas
            var mappedItems = pagedEntities.Items
                .Select(e => ClientesBusinessMapper.ToResponse(e.ToModel()))
                .ToList();

            // 3. Retornamos el nuevo PagedResult tipado con la respuesta de negocio
            return new PagedResult<ClienteResponse>(
                mappedItems,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize
            );
        }

        // ✅ CORREGIDO: Renombrado de RegistrarClienteAsync a CrearClienteAsync
        public async Task<ClienteResponse> CrearClienteAsync(CrearClienteRequest request)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
                throw new ValidationException("El nombre y apellido son obligatorios.");

            // Mapeo DTO -> DataModel
            var dataModel = ClientesBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            // Persistencia (Model -> Entity)
            var entity = dataModel.ToEntity();

            // Asignación explícita según tu entidad
            entity.usuario_id = request.UsuarioId;
            entity.nombre = request.Nombre;
            entity.apellido = request.Apellido;
            entity.telefono = request.Telefono;
            entity.documento_identidad = request.DocumentoIdentidad;

            await _unitOfWork.ClientesRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return ClientesBusinessMapper.ToResponse(dataModel);
        }

        public async Task<ClienteResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ClientesQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el cliente con ID: {id}");

            return ClientesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<ClienteResponse> ObtenerPorUsuarioIdAsync(Guid usuarioId)
        {
            var entity = await _unitOfWork.ClientesQueryRepository.ObtenerPorUsuarioIdAsync(usuarioId);
            if (entity == null)
                throw new NotFoundException($"No se encontró el perfil de cliente para el usuario ID: {usuarioId}");

            return ClientesBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task<ClienteResponse> ActualizarClienteAsync(ActualizarClienteRequest request)
        {
            var entity = await _unitOfWork.ClientesQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException($"No se encontró el cliente con ID: {request.Id}");

            entity.nombre = request.Nombre;
            entity.apellido = request.Apellido;
            entity.telefono = request.Telefono;
            entity.documento_identidad = request.DocumentoIdentidad;

            _unitOfWork.ClientesRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ClientesBusinessMapper.ToResponse(entity.ToModel());
        }
    }
}