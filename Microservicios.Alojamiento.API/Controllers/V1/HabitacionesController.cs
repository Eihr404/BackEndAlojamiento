using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Habitaciones;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/habitaciones")]
    public class HabitacionesController : ControllerBase
    {
        private readonly IHabitacionesService _service;

        public HabitacionesController(IHabitacionesService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HabitacionesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(ApiResponse<IEnumerable<HabitacionesResponse>>.Ok(await _service.ObtenerTodasAsync()));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<HabitacionesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<HabitacionesResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpGet("alojamiento/{alojamientoId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HabitacionesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAlojamiento(Guid alojamientoId) =>
            Ok(ApiResponse<IEnumerable<HabitacionesResponse>>.Ok(await _service.ObtenerPorAlojamientoAsync(alojamientoId)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<HabitacionesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CrearHabitacionesRequest request) =>
            Ok(ApiResponse<HabitacionesResponse>.Ok(await _service.CrearHabitacionAsync(request)));

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<HabitacionesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] ActualizarHabitacionesRequest request) =>
            Ok(ApiResponse<HabitacionesResponse>.Ok(await _service.ActualizarHabitacionAsync(request)));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarHabitacionAsync(id);
            return NoContent();
        }
    }
}