using Microservicios.Alojamiento.Business.DTOs.Auditoria;
using Microservicios.Alojamiento.DataAccess.Common;

public interface IAuditoriaService
{
    Task<AuditoriaResponse> GuardarLogAsync(CrearAuditoriaRequest request);
    Task<IEnumerable<AuditoriaResponse>> ObtenerLogsPorTablaAsync(string tabla);

    Task<PagedResult<AuditoriaResponse>> ObtenerLogsPaginadosAsync(int pagina, int registrosPorPagina);

}