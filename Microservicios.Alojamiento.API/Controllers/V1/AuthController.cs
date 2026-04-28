using Asp.Versioning;
using Microservicios.Alojamiento.API.Models.Common;
using Microservicios.Alojamiento.API.Models.Settings;
using Microservicios.Alojamiento.Business.DTOs.Auth;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Microservicios.Alojamiento.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;
    private readonly IUsuario_rolesService _usuarioRolesService; // ← declara el campo


    public AuthController(
        IAuthService authService,
        IOptions<JwtSettings> jwtOptions,
        IUsuario_rolesService usuarioRolesService) // ← inyecta en constructor
    {
        _authService = authService;
        _jwtSettings = jwtOptions.Value;
        _usuarioRolesService = usuarioRolesService; // ← asigna
    }

    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        // LoginAsync ya devuelve los nombres de roles correctamente
        var result = await _authService.LoginAsync(request, ct);

        // Generar token
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, result.UserName),
        new Claim(JwtRegisteredClaimNames.Email, result.CorreoElectronico),
            new Claim("usuarioId", result.UsuarioId.ToString()),  // ← AGREGAR ESTA LÍNEA
    };

        foreach (var role in result.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        result.Token = new JwtSecurityTokenHandler().WriteToken(token);
        result.ExpirationUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        return Ok(ApiResponse<LoginResponse>.Ok(result, "Autenticación exitosa."));
    }



    [HttpPost("registro-socio")]
    public async Task<IActionResult> RegistrarSocio([FromBody] LoginRequest request)
    {
        // Llamamos a un nuevo método en el servicio que asigne el rol de Administrador
        await _authService.RegistrarSocioAsync(request);
        return Ok(ApiResponse<bool>.Ok(true, "Socio registrado exitosamente"));
    }

    [HttpPost("registro")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromBody] LoginRequest request)
    {
        await _authService.RegistrarUsuarioAsync(request);
        // Devolvemos un true genérico envuelto en nuestra respuesta estándar
        return Ok(ApiResponse<bool>.Ok(true, "Usuario registrado exitosamente"));
    }
}