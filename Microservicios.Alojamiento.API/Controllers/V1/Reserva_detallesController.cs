using Asp.Versioning;
using Microservicios.Alojamiento.Business.DTOs.Reserva_detalles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Alojamiento.API.Models.Common;        // ✅ esto faltaba

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reserva-detalles")]
    public class ReservaDetallesController : ControllerBase
    {
        private readonly IReserva_detallesService _service;

        public ReservaDetallesController(IReserva_detallesService service) => _service = service;

        [HttpGet("reserva/{reservaId:guid}")]
        public async Task<IActionResult> GetByReserva(Guid reservaId) =>
            Ok(ApiResponse<IEnumerable<Reserva_detalleResponse>>.Ok(await _service.ObtenerDetallesPorReservaAsync(reservaId)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearReserva_detallesRequest request) =>
            Ok(ApiResponse<Reserva_detalleResponse>.Ok(await _service.AgregarDetalleReservaAsync(request)));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarDetalleReservaAsync(id);
            return NoContent();
        }
    }
}