using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Administradores;
using Microservicios.Alojamiento.Business.Interfaces;

namespace Microservicios.Alojamiento.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/administradores")]
public class AdministradoresController : ControllerBase
{
    private readonly IAdministradoresService _service;
    public AdministradoresController(IAdministradoresService service) => _service = service;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AdministradoresResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(ApiResponse<AdministradoresResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AdministradoresResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Post([FromBody] CrearAdministradoresRequest request) =>
    Ok(ApiResponse<AdministradoresResponse>.Ok(await _service.RegistrarAdministradorAsync(request)));

    [HttpPut]
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<AdministradoresResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] ActualizarAdministradoresRequest req)
    {
        var result = await _service.ActualizarAdministradorAsync(req);
        return Ok(ApiResponse<AdministradoresResponse>.Ok(result));
    }

    [HttpGet("por-usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AdministradoresResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUsuarioId(Guid usuarioId) =>
    Ok(ApiResponse<AdministradoresResponse>.Ok(
        await _service.ObtenerPorUsuarioIdAsync(usuarioId)));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.EliminarAdministradorAsync(id);
        return NoContent(); // 204 es perfecto para eliminaciones exitosas sin cuerpo
    }
}