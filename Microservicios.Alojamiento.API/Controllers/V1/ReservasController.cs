using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Reservas;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reservas")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservasService _service;

        public ReservasController(IReservasService service) => _service = service;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CrearReservaRequest request)
        {
            Console.WriteLine($">>> Reserva recibida: {System.Text.Json.JsonSerializer.Serialize(request)}");
            var result = await _service.CrearReservaAsync(request);
            return Ok(ApiResponse<ReservaResponse>.Ok(result));
        }
        [HttpGet("admin/{adminId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReservaResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAdmin(Guid adminId) =>
    Ok(ApiResponse<IEnumerable<ReservaResponse>>.Ok(
        await _service.ObtenerReservasPorAdminAsync(adminId)));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<ReservaResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpGet("cliente/{clienteId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReservaResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCliente(Guid clienteId) =>
            Ok(ApiResponse<IEnumerable<ReservaResponse>>.Ok(await _service.ObtenerReservasPorClienteAsync(clienteId)));

        [HttpPatch("{id:guid}/estado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchEstado(Guid id, [FromBody] string nuevoEstado)
        {
            await _service.CambiarEstadoReservaAsync(id, nuevoEstado);
            return NoContent();
        }

        [HttpPost("{id:guid}/cancelar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _service.CancelarReservaAsync(id);
            return NoContent();
        }
    }
}