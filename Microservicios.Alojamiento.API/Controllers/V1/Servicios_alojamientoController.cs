using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/servicios-alojamiento")]
    public class ServiciosAlojamientoController : ControllerBase
    {
        private readonly IServicios_alojamientoService _service;

        public ServiciosAlojamientoController(IServicios_alojamientoService service) => _service = service;

        [HttpGet("alojamiento/{alojamientoId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Servicios_alojamientoResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAlojamiento(Guid alojamientoId) =>
            Ok(ApiResponse<IEnumerable<Servicios_alojamientoResponse>>.Ok(await _service.ObtenerServiciosPorAlojamientoAsync(alojamientoId)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Servicios_alojamientoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CrearServicios_alojamientoRequest request) =>
            Ok(ApiResponse<Servicios_alojamientoResponse>.Ok(await _service.AsignarServicioAAlojamientoAsync(request)));

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<Servicios_alojamientoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] ActualizarServicios_alojamientoRequest request) =>
            Ok(ApiResponse<Servicios_alojamientoResponse>.Ok(await _service.ActualizarServicioAsignadoAsync(request)));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.QuitarServicioDeAlojamientoAsync(id);
            return NoContent();
        }
    }
}