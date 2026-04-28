using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Usuario_roles;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuario-roles")]
    public class UsuarioRolesController : ControllerBase
    {
        private readonly IUsuario_rolesService _service;

        public UsuarioRolesController(IUsuario_rolesService service) => _service = service;

        [HttpGet("usuario/{usuarioId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Usuario_rolesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsuario(Guid usuarioId) =>
            Ok(ApiResponse<IEnumerable<Usuario_rolesResponse>>.Ok(await _service.ObtenerRolesPorUsuarioAsync(usuarioId)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Usuario_rolesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CrearUsuario_rolesRequest request) =>
            Ok(ApiResponse<Usuario_rolesResponse>.Ok(await _service.AsignarRolAUsuarioAsync(request)));

        [HttpDelete("usuario/{usuarioId:guid}/rol/{rolId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid usuarioId, Guid rolId)
        {
            await _service.EliminarRolDeUsuarioAsync(usuarioId, rolId);
            return NoContent();
        }
    }
}