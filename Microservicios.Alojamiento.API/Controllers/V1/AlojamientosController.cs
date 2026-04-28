using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;        // ✅ ApiResponse
using Microservicios.Alojamiento.Business.DTOs.Alojamientos;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.DataAccess.Common;        // ✅ PagedResult
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/alojamientos")]
public class AlojamientosController : ControllerBase
{
    private readonly IAlojamientosService _service;

    public AlojamientosController(IAlojamientosService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<AlojamientoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaged([FromQuery] AlojamientoFiltroRequest filtro) =>
        Ok(ApiResponse<PagedResult<AlojamientoResponse>>.Ok(await _service.ObtenerFiltradosAsync(filtro)));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AlojamientoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(ApiResponse<AlojamientoResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AlojamientoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CrearAlojamientoRequest request)
    {
        // Para registrar en consola el request recibido (solo para depuración, no recomendado en producción)
        System.Diagnostics.Debug.WriteLine($">>> Request recibido: {System.Text.Json.JsonSerializer.Serialize(request)}");
        return Ok(ApiResponse<AlojamientoResponse>.Ok(await _service.CrearAlojamientoAsync(request)));
    }


    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<AlojamientoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] ActualizarAlojamientoRequest request) =>
        Ok(ApiResponse<AlojamientoResponse>.Ok(await _service.ActualizarAlojamientoAsync(request)));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.EliminarAlojamientoAsync(id);
        return NoContent();
    }
}