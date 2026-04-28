using Microservicios.Alojamiento.Business.DTOs.Auditoria;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


public class AuditoriaService : IAuditoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditoriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // 1. Corregido: Nombre sincronizado con la Interfaz
    public async Task<AuditoriaResponse> GuardarLogAsync(CrearAuditoriaRequest request)
    {
        var dataModel = AuditoriaBusinessMapper.ToDataModel(request);

        // No asignamos ID, dejamos que la BD (serial) lo genere
        var entity = dataModel.ToEntity();

        entity.usuario_id = request.UsuarioId;
        entity.accion = request.Accion;
        entity.tabla_afectada = request.TablaAfectada;

        // Aquí es donde ocurre la magia: 
        // Si registro_id es Guid? y el DTO es Guid?, esto es compatible.
        entity.registro_id = request.RegistroId;

        entity.datos_anteriores = request.DatosAnteriores;
        entity.fecha_hora = DateTime.UtcNow;

        await _unitOfWork.AuditoriaRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return AuditoriaBusinessMapper.ToResponse(dataModel);
    }

    // 2. Corregido: Implementación del filtro por tabla
    public async Task<IEnumerable<AuditoriaResponse>> ObtenerLogsPorTablaAsync(string tabla)
    {
        var entities = await _unitOfWork.AuditoriaQueryRepository.GetLogsByTableAsync(tabla);

        return entities.Select(e => AuditoriaBusinessMapper.ToResponse(e.ToModel()));
    }

    public async Task<PagedResult<AuditoriaResponse>> ObtenerLogsPaginadosAsync(int pagina, int registrosPorPagina)
    {
        var pagedEntities = await _unitOfWork.AuditoriaQueryRepository.GetLogsPagedAsync(pagina, registrosPorPagina);

        return new PagedResult<AuditoriaResponse>(
            pagedEntities.Items.Select(e => AuditoriaBusinessMapper.ToResponse(e.ToModel())).ToList(),
            pagedEntities.TotalCount,
            pagedEntities.CurrentPage,
            pagedEntities.PageSize);
    }


}