using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;      // ✅ esto faltaba
using Microservicios.Alojamiento.Business.DTOs.Facturas;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/facturas")]
public class FacturasController : ControllerBase
{
    private readonly IFacturasService _service;

    public FacturasController(IFacturasService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CrearFacturaRequest request) =>
        Ok(ApiResponse<FacturaResponse>.Ok(await _service.GenerarFacturaAsync(request)));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(ApiResponse<FacturaResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

    [HttpGet("reserva/{reservaId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByReserva(Guid reservaId) =>
        Ok(ApiResponse<FacturaResponse>.Ok(await _service.ObtenerFacturaPorReservaAsync(reservaId)));

    [HttpPatch("{id:guid}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PatchEstado(Guid id, [FromBody] string nuevoEstado)
    {
        await _service.ActualizarEstadoPagoAsync(id, nuevoEstado);
        return NoContent();
    }
}