using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Clientes;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.DataAccess.Common;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClientesService _service;

    public ClientesController(IClientesService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<ClienteResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] ClienteFiltroRequest request) =>
        Ok(ApiResponse<PagedResult<ClienteResponse>>.Ok(await _service.ObtenerFiltradosAsync(request)));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(ApiResponse<ClienteResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CrearClienteRequest request) =>
        Ok(ApiResponse<ClienteResponse>.Ok(await _service.CrearClienteAsync(request)));

    // ✅ Nuevo endpoint bien posicionado
    [HttpGet("usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUsuarioId(Guid usuarioId) =>
        Ok(ApiResponse<ClienteResponse>.Ok(await _service.ObtenerPorUsuarioIdAsync(usuarioId)));

    // ✅ Etiquetas conectadas correctamente a su método
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] ActualizarClienteRequest request) =>
        Ok(ApiResponse<ClienteResponse>.Ok(await _service.ActualizarClienteAsync(request)));
}