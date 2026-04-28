using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Metodos_pago;
using Microservicios.Alojamiento.Business.Interfaces;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/metodos-pago")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IMetodos_pagoService _service;

        public MetodosPagoController(IMetodos_pagoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(ApiResponse<IEnumerable<Metodo_pagoResponse>>.Ok(await _service.ObtenerTodosAsync()));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<Metodo_pagoResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearMetodo_pagoRequest request) =>
            Ok(ApiResponse<Metodo_pagoResponse>.Ok(await _service.CrearMetodoPagoAsync(request)));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ActualizarMetodos_pagoRequest request) =>
            Ok(ApiResponse<Metodo_pagoResponse>.Ok(await _service.ActualizarMetodoPagoAsync(request)));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarMetodoPagoAsync(id);
            return NoContent();
        }
    }
}