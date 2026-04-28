using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Servicios;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/servicios")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServiciosService _service;

        public ServiciosController(IServiciosService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ServiciosResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(ApiResponse<IEnumerable<ServiciosResponse>>.Ok(await _service.ObtenerTodosAsync()));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ServiciosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<ServiciosResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ServiciosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CrearServiciosRequest request) =>
            Ok(ApiResponse<ServiciosResponse>.Ok(await _service.CrearServicioAsync(request)));

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<ServiciosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] ActualizarServiciosRequest request) =>
            Ok(ApiResponse<ServiciosResponse>.Ok(await _service.ActualizarServicioAsync(request)));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarServicioAsync(id);
            return NoContent();
        }
    }
}