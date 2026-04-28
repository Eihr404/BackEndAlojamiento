using Asp.Versioning;
using Microservicios.Alojamiento.Business.DTOs.Resenas;
using Microservicios.Alojamiento.API.Models.Common;        // ✅ esto faltaba
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/resenas")]
    public class ResenasController : ControllerBase
    {
        private readonly IResenasService _service;

        public ResenasController(IResenasService service) => _service = service;

        [HttpGet("alojamiento/{alojamientoId:guid}")]
        public async Task<IActionResult> GetByAlojamiento(Guid alojamientoId) =>
            Ok(ApiResponse<IEnumerable<ResenaResponse>>.Ok(await _service.ObtenerResenasPorAlojamientoAsync(alojamientoId)));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<ResenaResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearResenaRequest request) =>
            Ok(ApiResponse<ResenaResponse>.Ok(await _service.PublicarResenaAsync(request)));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ActualizarResenasRequest request) =>
            Ok(ApiResponse<ResenaResponse>.Ok(await _service.ActualizarResenaAsync(request)));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarResenaAsync(id);
            return NoContent();
        }
    }
}