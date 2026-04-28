using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.Business.DTOs.Usuarios;
using Microservicios.Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Microservicios.Alojamiento.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _service;
        public UsuariosController(IUsuariosService service) => _service = service;

        // ✅ Agregamos prefijo "email/" para evitar colisiones con el ID (Guid)
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ApiResponse<UsuariosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email) =>
            Ok(ApiResponse<UsuariosResponse>.Ok(await _service.ObtenerPorEmailAsync(email)));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<UsuariosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(ApiResponse<UsuariosResponse>.Ok(await _service.ObtenerPorIdAsync(id)));

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UsuariosResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CrearUsuariosRequest req) =>
            Ok(ApiResponse<UsuariosResponse>.Ok(await _service.CrearUsuarioAsync(req)));

        [HttpPatch("{id:guid}/estado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchEstado(Guid id, [FromBody] bool activo)
        {
            await _service.CambiarEstadoAsync(id, activo);
            return NoContent(); // Más limpio y conforme a REST para una actualización parcial
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.EliminarUsuarioAsync(id);
            return NoContent();
        }
    }
}