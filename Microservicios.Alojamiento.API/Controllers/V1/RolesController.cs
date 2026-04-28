using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Roles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _service;

        public RolesController(IRolesService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RolesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(ApiResponse<IEnumerable<RolesResponse>>.Ok(await _service.ObtenerRolesAsync()));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<RolesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<RolesResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<RolesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Por si falla la validación de negocio/forma
        public async Task<IActionResult> Post([FromBody] CrearRolesRequest request) =>
            Ok(ApiResponse<RolesResponse>.Ok(await _service.CrearRolAsync(request)));

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<RolesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] ActualizarRolesRequest request) =>
            Ok(ApiResponse<RolesResponse>.Ok(await _service.ActualizarRolAsync(request)));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Ejecutamos la eliminación; si no existe, el servicio lanzará 
            // NotFoundException y el middleware se encargará de devolver el 404.
            await _service.EliminarRolAsync(id);

            // Si llegamos aquí, fue exitoso. Retornamos 204.
            return NoContent();
        }
    }
}