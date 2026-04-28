using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion;
using Microservicios.Alojamiento.Business.Interfaces;

namespace Microservicios.Alojamiento.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/alojamientos")] // Ruta base orientada al recurso principal
public class Alojamiento_habitacionController : ControllerBase
{
    private readonly IAlojamiento_habitacionService _service;

    public Alojamiento_habitacionController(IAlojamiento_habitacionService service) => _service = service;

    // GET: api/v1/alojamientos/{alojamientoId}/habitaciones
    [HttpGet("{alojamientoId:guid}/habitaciones")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Alojamiento_habitacionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHabitaciones(Guid alojamientoId) =>
        Ok(ApiResponse<IEnumerable<Alojamiento_habitacionResponse>>.Ok(await _service.ObtenerPreciosPorAlojamientoAsync(alojamientoId)));

    // POST: api/v1/alojamientos/habitaciones (Asignar)
    [HttpPost("habitaciones")]
    [ProducesResponseType(typeof(ApiResponse<Alojamiento_habitacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] CrearAlojamiento_habitacionRequest request) =>
        Ok(ApiResponse<Alojamiento_habitacionResponse>.Ok(await _service.AsignarHabitacionAAlojamientoAsync(request)));

    // PUT: api/v1/alojamientos/habitaciones
    [HttpPut("habitaciones")]
    [ProducesResponseType(typeof(ApiResponse<Alojamiento_habitacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] ActualizarAlojamiento_habitacionRequest request) =>
        Ok(ApiResponse<Alojamiento_habitacionResponse>.Ok(await _service.ActualizarPrecioOCantidadAsync(request)));

    // DELETE: api/v1/alojamientos/habitaciones/{id}
    [HttpDelete("habitaciones/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.EliminarRelacionAsync(id);
        return NoContent();
    }
}