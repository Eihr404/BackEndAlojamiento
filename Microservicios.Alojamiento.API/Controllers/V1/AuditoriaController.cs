using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Auditoria;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.DataAccess.Common;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auditoria")]
public class AuditoriaController : ControllerBase
{
    private readonly IAuditoriaService _service;
    public AuditoriaController(IAuditoriaService service) => _service = service;

    [HttpGet("tabla/{tabla}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AuditoriaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTable(string tabla) =>
            Ok(ApiResponse<IEnumerable<AuditoriaResponse>>.Ok(await _service.ObtenerLogsPorTablaAsync(tabla)));

    [HttpGet("paginado")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AuditoriaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaged([FromQuery] int pagina = 1, [FromQuery] int registrosPorPagina = 10) =>
        Ok(ApiResponse<PagedResult<AuditoriaResponse>>.Ok(await _service.ObtenerLogsPaginadosAsync(pagina, registrosPorPagina)));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CrearAuditoriaRequest req)
    {
        await _service.GuardarLogAsync(req);
        return Ok(ApiResponse<string>.Ok("Log registrado correctamente"));
    }


}